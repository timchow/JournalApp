﻿using Microsoft.AspNetCore.Identity;

namespace JournalApp.Models
{
	public class AppUser : IdentityUser
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string PictureUrl { get; set; }
		public string SocialId { get; set; }
	}
}