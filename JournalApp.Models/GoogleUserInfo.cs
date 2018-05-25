namespace JournalApp.Models
{
	public class GoogleUserInfo
	{
		public GoogleUserInfo(bool isMockuser)
		{
			if (isMockuser)
			{
				email = "mock@mock.com";
				email_verified = true;
				family_name = "Mockerson";
				given_name = "Mockal";
				locale = "en";
				name = "Mockal Mockerson";
				picture = "MockPictureUrl.png";
				sub = "MockSocialID";
			}
		}

		public string email { get; set; }
		public bool email_verified { get; set; }
		public string family_name { get; set; }
		public string given_name { get; set; }
		public string locale { get; set; }
		public string name { get; set; }
		public string picture { get; set; }
		public string sub { get; set; }
	}
}