using JournalApp.BLL;
using JournalApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace JournalApp.Controllers
{
	[Produces("application/json")]
	[Route("api/Journal/{journalId:int}/Page")]
	public class PageController : BaseController
	{
		public PageController(IConfiguration appSettings) : base(appSettings)
		{
		}

		[HttpDelete("{pageId:int}")]
		public IActionResult Delete([FromRoute]int journalId, [FromRoute]int pageId)
		{
			PageService pageService = new PageService(Settings.GetSection("AppSettings").GetSection("DefaultConnectionString").Value);

			int rowsAffected = pageService.DeletePageFromJournal(journalId, pageId);

			return Ok(rowsAffected);
		}

		[HttpGet]
		public IActionResult GetAllPagesForJournal([FromRoute]int journalId)
		{
			PageService pageService = new PageService(Settings.GetSection("AppSettings").GetSection("DefaultConnectionString").Value);
			List<Page> pageList = pageService.GetAllPagesForJournal(journalId);
			var test = this.HttpContext.User.Identity.Name;

			if (pageList.Count == 0)
				return NoContent();

			return Ok(pageList);
		}

		[HttpPost("{pageId:int?}")]
		public IActionResult Post([FromRoute]int journalId, [FromRoute]int pageId, [FromBody]Page page)
		{
			PageService pageService = new PageService(Settings.GetSection("AppSettings").GetSection("DefaultConnectionString").Value);
			int rowsAffected = pageService.UpsertPageForJournal(pageId, journalId, page);

			return Ok(rowsAffected);
		}
	}
}