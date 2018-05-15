using JournalApp.DAL;
using JournalApp.Models;
using System.Collections.Generic;

namespace JournalApp.BLL
{
	public class JournalService
	{
		private readonly JournalDM _journalDM;

		public JournalService(string connectionString)
		{
			_journalDM = new JournalDM(connectionString);
		}

		public List<Journal> GetAllJournalsForUser(int userId)
		{
			return _journalDM.GetAllJournalsForUser(userId);
		}

		public int UpsertJournalForUser(int userId, int journalId, Journal journal)
		{
			return _journalDM.UpsertJournalForUser(userId, journalId, journal);
		}

		public int DeleteJournalForUser(int userId, int journalId)
		{
			return _journalDM.DeleteJournalForUser(userId, journalId);
		}
	}
}