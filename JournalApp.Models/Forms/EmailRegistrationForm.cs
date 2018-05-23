using AutoMapper;

namespace JournalApp.Models.FormModels
{
	public class EmailRegistrationForm
	{
		public string Email { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Location { get; set; }
		public string Password { get; set; }
	}

	// Creates a mapping profile between EmailRegistraionForm and AppUser
	public class EmailRegistrationFormToAppUserProfile : Profile
	{
		public EmailRegistrationFormToAppUserProfile()
		{
			CreateMap<EmailRegistrationForm, AppUser>().ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Email));
		}
	}
}