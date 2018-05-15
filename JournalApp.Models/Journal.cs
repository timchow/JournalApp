using System.Collections.Generic;

namespace JournalApp.Models
{
	public class Journal
	{
		public string Description { get; set; }
		public int? Id {
			get
			{
				if (_id == 0) return null;

				return _id;
			}
			set => _id = (int) value;
		}
		public string ImagePath { get; set; }
		public List<Page> PageList { get; set; }
		public string Title { get; set; }
		public int UserId { get; set; }

		private int _id { get; set; }
	}
}