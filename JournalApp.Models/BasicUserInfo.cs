using AutoMapper;

namespace JournalApp.Models.AccessTokens
{
	public class BasicUserInfo
	{
		public string Email { get; set; }
		public string LastName { get; set; }
		public string FirstName { get; set; }
		public string Location { get; set; }
		public string Id { get; set; }
		public string PictureUrl { get; set; }
	}

	// Creates a mapping profile between EmailRegistraionForm and AppUser
	public class AppUserToBasicUserInfoMappingProfile : Profile
	{
		public AppUserToBasicUserInfoMappingProfile()
		{
			CreateMap<AppUser,BasicUserInfo >();//.ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Email));
		}
	}
}