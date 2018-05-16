using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JournalApp.Models.Tokens;
using Newtonsoft.Json;

namespace JournalApp.Auth
{
	public class Tokens
	{
		public static async Task<object> GenerateJwt(ClaimsIdentity identity, IJwtFactory jwtFactory, string userName, JwtIssuerOptions jwtOptions, JsonSerializerSettings serializerSettings)
		{
			var response = new
			{
				id = identity.Claims.Single(c => c.Type == "id").Value,
				auth_token = await jwtFactory.GenerateEncodedToken(userName, identity),
				expires_in = (int)jwtOptions.ValidFor.TotalSeconds
			};

			return response;
		}
	}
}
