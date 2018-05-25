using AutoMapper;
using JournalApp.Auth;
using JournalApp.Constants;
using JournalApp.DAL;
using JournalApp.Models;
using JournalApp.Models.AccessTokens;
using JournalApp.Models.FormModels;
using JournalApp.Models.Tokens;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JournalApp.Controllers
{
	[Produces("application/json")]
	[Route("api/Auth")]
	public class AuthController : BaseController
	{
		private readonly ApplicationDbContext _appDbContext;
		private readonly IJwtFactory _jwtFactory;
		private readonly JwtIssuerOptions _jwtOptions;
		private readonly IMapper _mapper;
		private readonly UserManager<AppUser> _userManager;
		private readonly bool MockOn;
		private readonly string MockToken;

		public AuthController(UserManager<AppUser> userManager, IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtOptions, ApplicationDbContext appDbContext, IMapper mapper,
			IConfiguration appSettings) : base(appSettings)
		{
			_userManager = userManager;
			_jwtFactory = jwtFactory;
			_jwtOptions = jwtOptions.Value;
			_appDbContext = appDbContext;
			_mapper = mapper;
			MockOn = bool.Parse(Settings.GetSection("MOCK").GetSection("ON").Value);
			MockToken = Settings.GetSection("MOCK").GetSection("TOKEN").Value;
		}

		[HttpPost("login/Facebook")]
		[EnableCors("AllowAllHeaders")]
		public async Task<IActionResult> LoginFacebookWithAccessToken([FromBody]GoogleAccessToken body)
		{
			FacebookUserInfo userInfo = null;
			if (MockOn && body.AccessToken == MockToken) // get value from config file
			{
				userInfo = new FacebookUserInfo(isMockuser: true);
			}
			else
			{
				HttpClient client = new HttpClient();

				// 1.generate an app access token
				(string, string)[] appAccessTokenRequestParameters = {
					("client_id", Settings.GetSection("Facebook").GetSection("ClientId").Value),
					("client_secret", Settings.GetSection("Facebook").GetSection("ClientSecret").Value),
					("grant_type", "client_credentials")
				};
				var queryParams = BuildQueryParameters(appAccessTokenRequestParameters);

				string appAccessTokenRequestUrl = $"{Urls.FACEBOOK_API_TOKEN_ACCESS}?{queryParams.ToString()}";
				var appAccessTokenResponse = await client.GetStringAsync(appAccessTokenRequestUrl);
				var appAccessToken = JsonConvert.DeserializeObject<FacebookAccessToken>(appAccessTokenResponse);

				// 2. validate the user access token
				(string, string)[] tokenInfoRequestParameters = {
					("input_token", body.AccessToken),
					("access_token", appAccessToken.access_token)
				};

				queryParams = BuildQueryParameters(tokenInfoRequestParameters);
				string tokenInfoRequestUrl = $"{Urls.FACEBOOK_API_TOKEN_VALIDATION}?{queryParams.ToString()}";
				var tokenInfoResponse = await client.GetStringAsync(tokenInfoRequestUrl);
				var tokenInfo = JsonConvert.DeserializeObject<FacebookAccessTokenInformation>(tokenInfoResponse);

				if (!tokenInfo.data.is_valid) return BadRequest("Token is not valid");

				// 3. request for user's info
				(string, string)[] userInfoRequestParameters = {
					("fields", "id,email,first_name,last_name,name,gender,locale,birthday,picture"),
					("access_token", body.AccessToken)
				};

				queryParams = BuildQueryParameters(userInfoRequestParameters);
				string userInfoRequestUrl = $"{Urls.FACEBOOK_API_USER_INFO}?{queryParams.ToString()}";
				var userInfoResponse = await client.GetStringAsync(userInfoRequestUrl);
				userInfo = JsonConvert.DeserializeObject<FacebookUserInfo>(userInfoResponse);
			}

			// Use the User's information to create a new AppUser in the system

			BasicUserInfo basicUserInfo = _mapper.Map<BasicUserInfo>(userInfo);

			var identityResult = await AddUserToApplication(basicUserInfo);
			if (identityResult != null && !identityResult.Succeeded) return new BadRequestObjectResult(identityResult.Errors);

			var localUser = await _userManager.FindByNameAsync(userInfo.email);

			if (localUser == null)
			{
				return BadRequest("Could not find user in the DB");
			}

			var response = await GenerateResponse(localUser);

			return new OkObjectResult(response);
		}

		[HttpPost("login/Google")]
		[EnableCors("AllowAllHeaders")]
		public async Task<IActionResult> LoginGoogleWithAccessToken([FromBody]GoogleAccessToken body)
		{
			GoogleUserInfo userInfo = null;
			if (body.AccessToken == "mock") // get value from config file
			{
				userInfo = new GoogleUserInfo(isMockuser:true);
			}
			else
			{
				// check to see if the accessToken is valid
				HttpClient client = new HttpClient();

				// 1. get token information and validate
				(string, string)[] tokenInfoRequestParameters = {
					("access_token", body.AccessToken)
				};

				var queryParams = BuildQueryParameters(tokenInfoRequestParameters);
				string tokenInfoRequestUrl = $"{Urls.GOOGLE_API_TOKEN_INFO}?{queryParams.ToString()}";
				var tokenInfoResponse = await client.GetStringAsync(tokenInfoRequestUrl);
				var tokenInfo = JsonConvert.DeserializeObject<GoogleAccessTokenInformation>(tokenInfoResponse);

				if (tokenInfo == null) return BadRequest("Invalid access token"); // TODO: Check the expiration time on the token as well

				// 2. get user's info

				// 3. get token information and validate
				(string, string)[] userInfoRequestParameters = {
					("alt", "json"),
					("access_token", body.AccessToken)
				};

				queryParams = BuildQueryParameters(userInfoRequestParameters);
				string userInfoRequestUrl = $"{Urls.GOOGLE_API_USER_INFO}?{queryParams.ToString()}";
				var userInfoResponse = await client.GetStringAsync(userInfoRequestUrl);
				userInfo = JsonConvert.DeserializeObject<GoogleUserInfo>(userInfoResponse);
			}

			BasicUserInfo basicUserInfo = _mapper.Map<BasicUserInfo>(userInfo);

			var identityResult = await AddUserToApplication(basicUserInfo);
			if (identityResult != null && !identityResult.Succeeded) return new BadRequestObjectResult(identityResult.Errors);

			var localUser = await _userManager.FindByNameAsync(userInfo.email);

			if (localUser == null)
			{
				return BadRequest("Could not find user in the DB");
			}

			var response = await GenerateResponse(localUser);

			return new OkObjectResult(response);
		}

		// POST api/auth/login
		[HttpPost("login")]
		[EnableCors("AllowAllHeaders")]
		public async Task<IActionResult> Post([FromBody]SignInForm credentials)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var identity = await GetClaimsIdentity(credentials.UserName, credentials.Password);
			if (identity == null)
			{
				return BadRequest("Invalid username or password.");
			}

			var localUser = await _userManager.FindByNameAsync(credentials.UserName);

			if (localUser == null)
			{
				return BadRequest("Could not find user in the DB");
			}

			var jwt = await Tokens.GenerateJwt(identity, _jwtFactory, credentials.UserName, _jwtOptions);

			BasicUserInfo basicUserInfo = _mapper.Map<BasicUserInfo>(localUser);
			var response = new List<object> { jwt, basicUserInfo };
			return new OkObjectResult(response);
		}

		private static NameValueCollection BuildQueryParameters(params (string, string)[] pairs)
		{
			NameValueCollection queryParams = System.Web.HttpUtility.ParseQueryString(string.Empty);
			foreach (var pair in pairs)
			{
				queryParams.Add(pair.Item1, pair.Item2);
			}

			return queryParams;
		}

		private async Task<IdentityResult> AddUserToApplication(BasicUserInfo userInfo)
		{
			var user = await _userManager.FindByEmailAsync(userInfo.Email);
			IdentityResult result = null;

			if (user == null)
			{
				var appUser = new AppUser
				{
					FirstName = userInfo.FirstName,
					LastName = userInfo.LastName,
					SocialId = userInfo.SocialId,
					Email = userInfo.Email,
					UserName = userInfo.Email,
					PictureUrl = userInfo.PictureUrl
				};

				result = await _userManager.CreateAsync(appUser, Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8));

				if (!result.Succeeded)
				{
					return result;
				}

				await _appDbContext.JournalOwners.AddAsync(new JournalOwner { IdentityId = appUser.Id });
				await _appDbContext.SaveChangesAsync();
			}

			return result;
		}

		private async Task<List<object>> GenerateResponse(AppUser localUser)
		{
			var jwt = await Tokens.GenerateJwt(_jwtFactory.GenerateClaimsIdentity(localUser.UserName, localUser.Id), _jwtFactory,
				localUser.UserName, _jwtOptions);
			BasicUserInfo basicUserInfo = _mapper.Map<BasicUserInfo>(localUser);
			var response = new List<object> { jwt, basicUserInfo };

			return response;
		}

		private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
		{
			if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
				return await Task.FromResult<ClaimsIdentity>(null);

			// get the user to verifty
			var userToVerify = await _userManager.FindByNameAsync(userName);

			if (userToVerify == null) return await Task.FromResult<ClaimsIdentity>(null);

			// check the credentials
			if (await _userManager.CheckPasswordAsync(userToVerify, password))
			{
				return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id));
			}

			// Credentials are invalid, or account doesn't exist
			return await Task.FromResult<ClaimsIdentity>(null);
		}
	}
}