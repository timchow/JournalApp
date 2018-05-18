using JournalApp.BLL;
using JournalApp.BLL.Interfaces;
using JournalApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace JournalApp.Controllers
{
	[Produces("application/json")]
	[Route("api/Page/{pageId:int}/BulletPoint")]
	public class BulletPointController : BaseController
	{
		private readonly IBulletPointService _bulletPointService;

		public BulletPointController(IConfiguration appSettings, IBulletPointService bulletPointService) : base(appSettings)
		{
			_bulletPointService = bulletPointService;
		}

		[HttpDelete("{bulletPointId:int}")]
		public IActionResult Delete([FromRoute]int pageId, [FromRoute]int bulletPointId)
		{
			BulletPointService bulletPointService;

			try
			{
				bulletPointService = new BulletPointService(Settings.GetSection("AppSettings").GetSection("DefaultConnectionString").Value);
				bulletPointService.DeleteBulletPointForPage(pageId, bulletPointId, false);
			}
			catch (Exception e)
			{
				return BadRequest(e);
			}

			return Ok();
		}

		[HttpGet]
		public IActionResult GetBulletPointsForPage([FromRoute]int pageId)
		{
			BulletPointService bulletPointService;
			List<BulletPoint> bulletPointList;

			try
			{
				bulletPointService = new BulletPointService(Settings.GetSection("AppSettings").GetSection("DefaultConnectionString").Value);
				bulletPointList = bulletPointService.GetAllBulletPointsForPage(pageId);
			}
			catch (Exception e)
			{
				return BadRequest(e);
			}

			if (bulletPointList.Count == 0)
				return NoContent();

			return Ok(bulletPointList);
		}

		[HttpPost("{bulletPointId:int?}")]
		public IActionResult UpsertBulletPointToPage([FromRoute]int pageId, [FromRoute]int bulletPointId, [FromBody]BulletPoint data)
		{
			BulletPointService bulletPointService = new BulletPointService(Settings.GetSection("AppSettings").GetSection("DefaultConnectionString").Value);
			bulletPointService.UpsertBulletPointForPage(pageId, bulletPointId, data);

			return Ok();
		}
	}
}