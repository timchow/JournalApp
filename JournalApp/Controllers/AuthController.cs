﻿using JournalApp.Auth;
using JournalApp.Constants;
using JournalApp.Models;
using JournalApp.Models.AccessTokens;
using JournalApp.Models.FormModels;
using JournalApp.Models.Tokens;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JournalApp.Controllers
{
	[Produces("application/json")]
	[Route("api/Auth")]
	public class AuthController : Controller
	{
		private readonly IJwtFactory _jwtFactory;
		private readonly JwtIssuerOptions _jwtOptions;
		private readonly UserManager<AppUser> _userManager;

		public AuthController(UserManager<AppUser> userManager, IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtOptions)
		{
			_userManager = userManager;
			_jwtFactory = jwtFactory;
			_jwtOptions = jwtOptions.Value;
		}

		[HttpPost("login/Google")]
		[EnableCors("AllowAllHeaders")]
		public async Task<IActionResult> LoginGoogleWithAccessToken([FromBody]GoogleAccessToken body)
		{
			// check to see if the accessToken is valid
			HttpClient client = new HttpClient();
			string tokenInfoRequestUrl = $"{Urls.GOOGLE_API_TOKEN_INFO}?access_token={body.AccessToken}";
			var userAccessTokenValidationResponse = await client.GetStringAsync(tokenInfoRequestUrl);
			var appAccessToken = JsonConvert.DeserializeObject<GoogleAccessTokenInformation>(userAccessTokenValidationResponse);

			if (appAccessToken == null) return BadRequest("Invalid access token"); // TODO: Check the expiration time on the token as well

			// Use the accessToken to request for the User's information
			string userInfoRequestUrl = $"{Urls.GOOGLE_API_USER_INFO}?alt=json&access_token={body.AccessToken}";
			var userInformationResponse =
				await client.GetStringAsync(userInfoRequestUrl);
			var userInfo = JsonConvert.DeserializeObject<GoogleUserInformation>(userInformationResponse);

			// Use the User's information to create a new User in the system

			var user = await _userManager.FindByEmailAsync(userInfo.email);

			if (user == null)
			{
				var appUser = new AppUser
				{
					FirstName = userInfo.given_name,
					LastName = userInfo.family_name,
					SocialId = userInfo.sub,
					Email = userInfo.email,
					UserName = userInfo.email,
					PictureUrl = userInfo.picture
				};

				var result =
					await _userManager.CreateAsync(appUser, Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8));

				if (!result.Succeeded) return new BadRequestObjectResult("Error creating User!");
			}

			var localUser = await _userManager.FindByNameAsync(userInfo.email);

			if (localUser == null)
			{
				return BadRequest("Could not find user in the DB");
			}

			var jwt = await Tokens.GenerateJwt(_jwtFactory.GenerateClaimsIdentity(localUser.UserName, localUser.Id), _jwtFactory, localUser.UserName, _jwtOptions);
			var response = new List<object> { jwt, localUser };

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

			var jwt = await Tokens.GenerateJwt(identity, _jwtFactory, credentials.UserName, _jwtOptions);
			return new OkObjectResult(jwt);
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