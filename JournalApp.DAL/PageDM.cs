using JournalApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace JournalApp.DAL
{
	public class PageDM : DM,IPageDM
	{
		private readonly SqlConnection _sqlConnection;
		private string _connectionString;

		public PageDM(string connectionString)
		{
			_connectionString = connectionString;
			_sqlConnection = new SqlConnection(_connectionString);
		}

		public List<Page> GetAllPagesForJournal(int journalId, bool isActive = true)
		{
			List<Page> pageList = new List<Page>();

			using (var connection = _sqlConnection)
			{
				connection.Open();

				SqlCommand command = new SqlCommand("PageGetForJournal", connection);
				command.CommandType = CommandType.StoredProcedure;
				command.Parameters.Add(new SqlParameter("@journalId", journalId));
				command.Parameters.Add(new SqlParameter("@isActive", isActive));

				using (var reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						var page = new Page
						{
							JournalId = DBValueToInt32(reader["JournalId"]),
							Title = DBValueToString(reader["Title"]),
							Id = DBValueToInt32(reader["PageId"])
						};
						pageList.Add(page);
					}
				}
			}

			return pageList;
		}

		public int DeletePageFromJournal(int journalId, int pageId)
		{
			int rowsAffected = 0;

			using (var connection = _sqlConnection)
			{
				connection.Open();

				SqlCommand command = new SqlCommand("PageDeleteForJournal", connection);
				command.CommandType = CommandType.StoredProcedure;
				command.Parameters.Add(new SqlParameter("@journalId", journalId));
				command.Parameters.Add(new SqlParameter("@pageId", pageId));
				
				rowsAffected = command.ExecuteNonQuery();
			}

			return rowsAffected;
		}

		public int UpsertPageForJournal(int pageId, int journalId, Page page)
		{
			int rowsAffected = 0;

			using (var connection = _sqlConnection)
			{
				connection.Open();

				SqlCommand command = new SqlCommand("PageUpsertForJournal", connection);
				command.CommandType = CommandType.StoredProcedure;
				command.Parameters.Add(new SqlParameter("@pageId", pageId));
				command.Parameters.Add(new SqlParameter("@journalId", journalId));
				command.Parameters.Add(new SqlParameter("@newTitle", page.Title));
				command.Parameters.Add(new SqlParameter("@newJournalId", page.JournalId));
				rowsAffected = command.ExecuteNonQuery();
			}

			return rowsAffected;
		}
	
	}
}