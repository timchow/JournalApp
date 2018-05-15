using JournalApp.Models;
using System.Collections.Generic;

namespace JournalApp.DAL
{
	public interface IPageDM
	{
		List<Page> GetAllPagesForJournal(int journalId, bool isActive = true);
		int UpsertPageForJournal(int pageId, int journalId, Page page);
		int DeletePageFromJournal(int journalId, int pageId);
	}
}