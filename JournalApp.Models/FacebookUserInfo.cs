namespace JournalApp.Models
{
	public class Data
	{
		public Data(string pictureUrl)
		{
			url = pictureUrl;
		}

		public int height { get; set; }
		public bool is_silhouette { get; set; }
		public string url { get; set; }
		public int width { get; set; }
	}

	public class FacebookUserInfo
	{
		public FacebookUserInfo(bool isMockuser)
		{
			if (isMockuser)
			{
				email = "fbmock@mock.com";
				first_name = "FB Mockal";
				last_name = "Mockerson";
				id = "Mock FB Id";
				name = "FB Mockal Mockerson";
				picture = new Picture("MockFBPictureUrl.png");
			}
		}

		public string email { get; set; }
		public string first_name { get; set; }
		public string id { get; set; }
		public string last_name { get; set; }
		public string name { get; set; }
		public Picture picture { get; set; }
	}

	public class Picture
	{
		public Picture(string pictureUrl)
		{
			data = new Data(pictureUrl);
		}

		public Data data { get; set; }
	}
}