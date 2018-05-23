namespace JournalApp.Models
{
	public class JournalOwner
	{
		public string Gender { get; set; }
		public int Id { get; set; }
		public AppUser Identity { get; set; }
		public string IdentityId { get; set; }
		public string Locale { get; set; }

		// navigation property
		public string Location { get; set; }
	}
}