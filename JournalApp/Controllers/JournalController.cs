using JournalApp.BLL;
using JournalApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.Extensions.Configuration;

namespace JournalApp.Controllers
{
	[Produces("application/json")]
	[Route("api/User/{userId:int}/Journal")]
	public class JournalController : BaseController
	{
		public JournalController(IConfiguration appSettings) : base(appSettings)
		{
		}

		[HttpDelete("{journalId:int}")]
		public IActionResult Delete([FromRoute]int userId, [FromRoute]int journalId)
		{
			JournalService journalService = new JournalService(Settings.GetSection("AppSettings").GetSection("DefaultConnectionString").Value);
			int rowsAffected = journalService.DeleteJournalForUser(userId,journalId);

			return Ok(rowsAffected);
		}

		[HttpGet]
		public IActionResult GetAllJournalsForUser([FromRoute] int userId)
		{
			JournalService journalService = new JournalService(Settings.GetSection("AppSettings").GetSection("DefaultConnectionString").Value);
			List<Journal> journalList = journalService.GetAllJournalsForUser(userId);

			if (journalList.Count == 0)
				return NoContent();

			//HttpContext.Authentication.SignOutAsync("Cookies")
			
			return Ok(journalList);
		}

		[HttpPost("{journalId:int?}")]
		public IActionResult UpsertJournalForUser([FromRoute]int userId, [FromRoute]int journalId, [FromBody]Journal journal)
		{
			JournalService journalService = new JournalService(Settings.GetSection("AppSettings").GetSection("DefaultConnectionString").Value);
			int rowsAffected = journalService.UpsertJournalForUser(userId,journalId,journal);


			return Ok(rowsAffected);
		}
	}
}