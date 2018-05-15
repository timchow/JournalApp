using System;
using System.Collections.Generic;
using System.Text;
using JournalApp.Models;

namespace JournalApp.DAL
{
    public interface IJournalDM
    {
	    List<Journal> GetAllJournalsForUser(int userId, bool isActive = true);
	    int DeleteJournalForUser(int userId, int journalId);


    }
}
