using AutoMapper;
using JournalApp.Auth;
using JournalApp.DAL;
using JournalApp.Models;
using JournalApp.Models.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IO;
using System.Text;

namespace JournalApp
{
	public class Startup
	{
		private const string SecretKey = "iNivDmHLpUA223sqsfhqGbMRdRj1PVkH"; // Change this
		private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
			CopyMockSettingsToReactApp(@"..\JournalApp.React\web.config.js");
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			app.UseCors("AllowAllHeaders");
			app.UseAuthentication();
			app.UseMvc();
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			// Get options from app settings
			var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));

			#region Dependency Injections - Controller

			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
					b => b.MigrationsAssembly("JournalApp")));

			services.Configure<JwtIssuerOptions>(options =>
			{
				options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
				options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
				options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
			});

			// add identity
			var builder = services.AddIdentityCore<AppUser>(o =>
			{
				// configure identity options
				o.Password.RequireDigit = false;
				o.Password.RequireLowercase = false;
				o.Password.RequireUppercase = false;
				o.Password.RequireNonAlphanumeric = false;
				o.Password.RequiredLength = 6;
			});

			builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);
			builder.AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

			services.AddSingleton<IJwtFactory, JwtFactory>();
			services.AddSingleton<IConfiguration>(Configuration);

			#endregion Dependency Injections - Controller

			#region Authentication settings

			// This is how we know what a valid auth token is!
			var tokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuer = true,
				ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

				ValidateAudience = true,
				ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

				ValidateIssuerSigningKey = true,
				IssuerSigningKey = _signingKey,

				RequireExpirationTime = false,
				ValidateLifetime = true,
				ClockSkew = TimeSpan.Zero
			};

			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(configureOptions =>
			{
				configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
				configureOptions.TokenValidationParameters = tokenValidationParameters;
				configureOptions.SaveToken = true;
			});

			#endregion Authentication settings

			#region Policies

			services.AddAuthorization(options =>
			{
				options.AddPolicy("ApiUser", policy => policy.RequireClaim("rol", "api_access"));
			});

			#endregion Policies

			#region CORS settings

			services.AddCors(options =>
			{
				options.AddPolicy("AllowAllHeaders",
					b =>
					{
						b.AllowAnyOrigin()
							.AllowAnyHeader()
							.AllowAnyMethod();
					});
			});

			#endregion CORS settings

			services.AddAutoMapper();
			services.AddMvc();
		}

		#region Mock settings copy helpers

		private string AddCommaIfNeeded(string settingsFile, int mockSettingsInsertIdx)
		{
			string secondToLastBracketToEnd = settingsFile.Substring(mockSettingsInsertIdx);

			bool checkForComma = secondToLastBracketToEnd.Contains(",");
			return !checkForComma ? settingsFile.Insert(mockSettingsInsertIdx + 1, ",") : settingsFile;
		}

		private void AddMockSettings(string settingsFile)
		{
			int mockSettingsInsertIdx = GetMockSettingsInsertIdx(settingsFile);

			settingsFile = AddCommaIfNeeded(settingsFile, mockSettingsInsertIdx);
			mockSettingsInsertIdx = GetMockSettingsInsertIdx(settingsFile);

			var mockOn = Configuration.GetSection("MOCK").GetSection("ON").Value;
			var mockToken = Configuration.GetSection("MOCK").GetSection("TOKEN").Value;
			string mockText = $"MOCK: {{ ON: {mockOn.ToLower()}, TOKEN: \"{mockToken}\" }}";
			settingsFile = settingsFile.Insert(mockSettingsInsertIdx + 1, mockText);
			System.IO.File.WriteAllText(@"..\JournalApp.React\web.config.js", settingsFile);
		}

		private void CopyMockSettingsToReactApp(string reactWebConfigPath)
		{
			var settingsFile = File.ReadAllText(reactWebConfigPath);
			bool mockSettingsExist = settingsFile.Contains("MOCK");

			if (mockSettingsExist)
			{
				settingsFile = RemoveMockSettings(settingsFile);
			}

			AddMockSettings(settingsFile);
		}

		private int GetMockSettingsInsertIdx(string settingsFile)
		{
			string startToLastBracket = settingsFile.Substring(0, settingsFile.LastIndexOf("}"));
			int startToSecondToLastBracketIdx = startToLastBracket.LastIndexOf("}");
			string secondToLastBracketToEnd = startToLastBracket.Substring(startToSecondToLastBracketIdx);

			bool checkForComma = secondToLastBracketToEnd.Contains(",");

			if (checkForComma) return startToSecondToLastBracketIdx + secondToLastBracketToEnd.IndexOf(",");

			return startToSecondToLastBracketIdx;
		}

		private string RemoveMockSettings(string settingsFile)
		{
			int mockSettingsObjectIdx = settingsFile.IndexOf("MOCK");
			string partialSettingsFile = settingsFile.Substring(mockSettingsObjectIdx);
			int mockSettingsBlockEndIdx = partialSettingsFile.IndexOf("}");

			return settingsFile.Remove(mockSettingsObjectIdx, (mockSettingsObjectIdx + mockSettingsBlockEndIdx) - mockSettingsObjectIdx + 1);
		}

		#endregion Mock settings copy helpers
	}
}