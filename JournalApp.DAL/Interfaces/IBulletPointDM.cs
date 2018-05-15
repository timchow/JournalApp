using JournalApp.Models;
using System.Collections.Generic;

namespace JournalApp.DAL
{
	public interface IBulletPointDM
	{
		List<BulletPoint> GetAllBulletPointsForPage(int pageId, bool isActive = true);
		int UpsertBulletPointForPage(int pageId, int bulletPointId, BulletPoint data);
		int DeleteBulletPointForPage(int pageId, int bulletPointId, bool permanentDelete);
	}
}