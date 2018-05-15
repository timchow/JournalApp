namespace JournalApp.Models
{
	public class BulletPoint
	{
		public BulletPoint()
		{
		}

		public string Content
		{
			get => _Content;
			set => _Content = value;
		}

		public int? Id
		{
			get
			{
				if (_id == 0) return null;

				return _id;
			}
			set => _id = (int)value;
		}

		public int? PageId
		{
			get
			{
				if (_pageId == 0) return null;

				return _pageId;
			}
			set => _pageId = (int)value;
		}

		private string _Content { get; set; }
		private int _id { get; set; }
		private int _pageId { get; set; }
	}
}