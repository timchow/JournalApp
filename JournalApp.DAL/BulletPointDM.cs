using JournalApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace JournalApp.DAL
{
	public class BulletPointDM : DM,IBulletPointDM
	{
		private readonly SqlConnection _sqlConnection;
		private string _connectionString;

		public BulletPointDM(string connectionString)
		{
			_connectionString = connectionString;
			_sqlConnection = new SqlConnection(_connectionString);
		}

		public List<BulletPoint> GetAllBulletPointsForPage(int pageId, bool isActive = true)
		{
			List<BulletPoint> bulletPointlList = new List<BulletPoint>();

			using (var connection = _sqlConnection)
			{
				connection.Open();

				SqlCommand command = new SqlCommand("BulletPointGetForPage", connection);
				command.CommandType = CommandType.StoredProcedure;
				command.Parameters.Add(new SqlParameter("@pageId", pageId));
				command.Parameters.Add(new SqlParameter("@isActive", isActive));

				using (var reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						var bulletPoint = new BulletPoint
						{
							Id = DBValueToInt32(reader["BulletPointId"]),
							PageId = DBValueToInt32(reader["PageId"]),
							Content = DBValueToString(reader["Content"]),
						};
						bulletPointlList.Add(bulletPoint);
					}
				}
			}

			return bulletPointlList;
		}

		public int DeleteBulletPointForPage(int pageId, int bulletPointId, bool permanentDelete)
		{
			int rowsAffected = 0;

			using (var connection = _sqlConnection)
			{
				connection.Open();

				SqlCommand command = new SqlCommand("BulletPointDeleteForPage", connection);
				command.CommandType = CommandType.StoredProcedure;
				command.Parameters.Add(new SqlParameter("@pageId", pageId));
				command.Parameters.Add(new SqlParameter("@bulletPointId", bulletPointId));
				command.Parameters.Add(new SqlParameter("@permanentDelete", permanentDelete));
				command.Parameters.Add(new SqlParameter("@modifiedBy", "Test"));
				command.Parameters.Add(new SqlParameter("@modifiedDate", DateTime.Now));
				rowsAffected = command.ExecuteNonQuery();
			}

			return rowsAffected;
		}

		public int UpsertBulletPointForPage(int pageId, int bulletPointId, BulletPoint data)
		{
			int rowsAffected = 0;

			using (var connection = _sqlConnection)
			{
				connection.Open();

				SqlCommand command = new SqlCommand("BulletPointUpsertForPage", connection);
				command.CommandType = CommandType.StoredProcedure;
				command.Parameters.Add(new SqlParameter("@pageId", pageId));
				command.Parameters.Add(new SqlParameter("@bulletPointId", bulletPointId));
				command.Parameters.Add(new SqlParameter("@newContent", data.Content));
				command.Parameters.Add(new SqlParameter("@newPageId", data.PageId));
				rowsAffected = command.ExecuteNonQuery();
			}

			return rowsAffected;
		}
	}
}