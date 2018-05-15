using JournalApp.DAL;
using JournalApp.Models;
using System.Collections.Generic;
using System;

namespace JournalApp.BLL
{
	public class PageService
	{
		private readonly PageDM _pageDM;

		public PageService()
		{
			_pageDM = new PageDM("");
		}

		public PageService(string connectionString)
		{
			_pageDM = new PageDM(connectionString);
		}

		public List<Page> GetAllPagesForJournal(int journalId, bool isActive = true)
		{
			return _pageDM.GetAllPagesForJournal(journalId, isActive);
		}

		public int UpsertPageForJournal(int pageId, int journalId, Page page)
		{
			return _pageDM.UpsertPageForJournal(pageId, journalId, page);
		}

		public int DeletePageFromJournal(int journalId, int pageId)
		{
			return _pageDM.DeletePageFromJournal(journalId, pageId);
		}
	}
}