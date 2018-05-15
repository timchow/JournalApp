using JournalApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace JournalApp.DAL
{
	public class JournalDM : IJournalDM
	{
		private readonly SqlConnection _sqlConnection;
		private string _connectionString;

		public JournalDM(string connectionString)
		{
			_connectionString = connectionString;
			_sqlConnection = new SqlConnection(_connectionString);
		}

		public List<Journal> GetAllJournalsForUser(int userId, bool isActive = true)
		{
			List<Journal> journalList = new List<Journal>();

			using (var connection = _sqlConnection)
			{
				connection.Open();

				SqlCommand command = new SqlCommand("JournalGetForUser", connection);
				command.CommandType = CommandType.StoredProcedure;
				command.Parameters.Add(new SqlParameter("@userId", userId));
				command.Parameters.Add(new SqlParameter("@isActive", isActive));

				using (var reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						var journal = new Journal
						{
							Id = Int32.Parse(reader["JournalId"].ToString()),
							UserId = Int32.Parse(reader["Userid"].ToString()),
							Title = reader["Title"].ToString(),
							Description = reader["Description"].ToString(),
							ImagePath = reader["ImagePath"].ToString()
						};
						journalList.Add(journal);
					}
				}

				return journalList;
			}
		}

		public int UpsertJournalForUser(int userId, int journalId, Journal journal)
		{
			int rowsAffected = 0;

			using (var connection = _sqlConnection)
			{
				connection.Open();

				SqlCommand command = new SqlCommand("JournalUpsertForUser", connection);
				command.CommandType = CommandType.StoredProcedure;
				command.Parameters.Add(new SqlParameter("@journalId", journalId));
				command.Parameters.Add(new SqlParameter("@userId", userId));
				command.Parameters.Add(new SqlParameter("@newImagePath", journal.ImagePath));
				command.Parameters.Add(new SqlParameter("@newTitle", journal.Title));
				command.Parameters.Add(new SqlParameter("@newDescription", journal.Description));
				rowsAffected = command.ExecuteNonQuery();
			}

			return rowsAffected;
		}

		public int DeleteJournalForUser(int userId, int journalId)
		{
			int rowsAffected = 0;

			using (var connection = _sqlConnection)
			{
				connection.Open();

				SqlCommand command = new SqlCommand("JournalDeleteForUser", connection);
				command.CommandType = CommandType.StoredProcedure;
				command.Parameters.Add(new SqlParameter("@userId", userId));
				command.Parameters.Add(new SqlParameter("@journalId", journalId));

				rowsAffected = command.ExecuteNonQuery();
			}

			return rowsAffected;
		}
	}
}