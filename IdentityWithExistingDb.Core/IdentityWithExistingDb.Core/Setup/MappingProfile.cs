using AutoMapper;
using IdentityWithExistingDb.Core.Models.Security;
using IdentityWithExistingDb.Core.ViewModels.Security;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityWithExistingDb.Core.Setup
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UsersViewModel>();
            CreateMap<RoleFormViewModel, IdentityRole> ()
                .ForMember(dest => dest.Name, src => src.MapFrom(src => src.RoleName))
                .ForMember(dest => dest.NormalizedName, src => src.MapFrom(src => src.RoleName.Trim().ToUpper()))
                .ReverseMap();

            CreateMap<AddUserViewModel, User>();
        }
    }
}
