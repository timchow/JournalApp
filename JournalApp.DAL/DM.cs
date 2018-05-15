using System;

namespace JournalApp.DAL
{
	public class DM
	{
		protected int DBValueToInt32(object v)
		{
			if (v == null || v == DBNull.Value)
			{
				return 0;
			}

			if (v is Int32)
			{
				return (int)v;
			}

			return 0;
		}

		protected string DBValueToString(object v)
		{
			string result = string.Empty;

			if (v == null || v == DBNull.Value)
			{
				return result;
			}

			if (v is string)
			{
				result = v.ToString();
			}

			return result;
		}
	}
}