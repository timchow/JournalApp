using JournalApp.BLL;
using JournalApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

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
			int rowsAffected = journalService.DeleteJournalForUser(userId, journalId);

			return Ok(rowsAffected);
		}

		[Authorize(Policy = "ApiUser")]
		[HttpGet]
		public IActionResult GetAllJournalsForUser([FromRoute] int userId)
		{
			JournalService journalService = new JournalService(Settings.GetSection("AppSettings").GetSection("DefaultConnectionString").Value);
			List<Journal> journalList = journalService.GetAllJournalsForUser(userId);

			if (journalList.Count == 0)
				return NoContent();

			return Ok(journalList);
		}

		[HttpPost("{journalId:int?}")]
		public IActionResult UpsertJournalForUser([FromRoute]int userId, [FromRoute]int journalId, [FromBody]Journal journal)
		{
			JournalService journalService = new JournalService(Settings.GetSection("AppSettings").GetSection("DefaultConnectionString").Value);
			int rowsAffected = journalService.UpsertJournalForUser(userId, journalId, journal);

			return Ok(rowsAffected);
		}
	}
}