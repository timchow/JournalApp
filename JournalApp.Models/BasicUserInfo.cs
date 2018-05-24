using AutoMapper;

namespace JournalApp.Models
{
	public class BasicUserInfo
	{
		public string Email { get; set; }
		public string FirstName { get; set; }
		public string Id { get; set; }
		public string LastName { get; set; }
		public string Location { get; set; }
		public string PictureUrl { get; set; }
		public string SocialId { get; set; }
	}

	// Creates a mapping profile between x and BasicUserInfo
	// Maps AppUser(source) to BasicUserInfo(result)
	public class BasicUserInfoMappingProfiles : Profile
	{
		public BasicUserInfoMappingProfiles()
		{
			CreateMap<AppUser, BasicUserInfo>();

			CreateMap<GoogleUserInfo, BasicUserInfo>()
				.ForMember(au => au.Email, map => map.MapFrom(vm => vm.email))
				.ForMember(au => au.FirstName, map => map.MapFrom(vm => vm.given_name))
				.ForMember(au => au.LastName, map => map.MapFrom(vm => vm.family_name))
				.ForMember(au => au.SocialId, map => map.MapFrom(vm => vm.sub))
				.ForMember(au => au.PictureUrl, map => map.MapFrom(vm => vm.picture));

			CreateMap<FacebookUserInfo, BasicUserInfo>()
				.ForMember(au => au.Email, map => map.MapFrom(vm => vm.email))
				.ForMember(au => au.FirstName, map => map.MapFrom(vm => vm.first_name))
				.ForMember(au => au.LastName, map => map.MapFrom(vm => vm.first_name))
				.ForMember(au => au.SocialId, map => map.MapFrom(vm => vm.id))
				.ForMember(au => au.PictureUrl, map => map.MapFrom(vm => vm.picture.data.url));
		}
	}
}