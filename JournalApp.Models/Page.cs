using System.Collections.Generic;

namespace JournalApp.Models
{
	public class Page
	{
		public List<BulletPoint> BulletPointList { get; set; }

		public int? Id
		{
			get
			{
				if (_id == 0) return null;

				return _id;
			}
			set => _id = (int)value;
		}

		public int? JournalId
		{
			get
			{
				if (_journalId == 0) return null;

				return _journalId;
			}
			set => _journalId = (int)value;
		}

		public string Title { get; set; }
		private int _id { get; set; }
		private int _journalId { get; set; }
	}
}