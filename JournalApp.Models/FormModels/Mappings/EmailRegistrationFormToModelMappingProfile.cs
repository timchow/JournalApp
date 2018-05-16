using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using JournalApp.Models;

namespace JournalApp.Models.FormModels.Mappings
{
	public class EmailRegistrationFormToModelMappingProfile : Profile
	{
		public EmailRegistrationFormToModelMappingProfile()
		{
			CreateMap<EmailRegistrationForm, AppUser>().ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Email));
		}
	}
}
