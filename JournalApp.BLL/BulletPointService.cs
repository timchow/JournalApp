using JournalApp.DAL;
using JournalApp.Models;
using System.Collections.Generic;
using System;
using JournalApp.BLL.Interfaces;

namespace JournalApp.BLL
{
	public class BulletPointService : IBulletPointService
	{
		private readonly BulletPointDM _bulletPointDM;

		public BulletPointService()
		{
			_bulletPointDM = new BulletPointDM("");
		}

		public BulletPointService(string connectionString)
		{
			_bulletPointDM = new BulletPointDM(connectionString);
		}

		public List<BulletPoint> GetAllBulletPointsForPage(int pageId, bool isActive = true)
		{
			return _bulletPointDM.GetAllBulletPointsForPage(pageId, isActive);
		}

		public void UpsertBulletPointForPage(int pageId, int bulletPointId, BulletPoint data)
		{
			_bulletPointDM.UpsertBulletPointForPage(pageId, bulletPointId, data);
		}

		public void DeleteBulletPointForPage(int pageId, int bulletPointId, bool permanentDelete)
		{
			_bulletPointDM.DeleteBulletPointForPage(pageId, bulletPointId, permanentDelete);
		}
	}
}