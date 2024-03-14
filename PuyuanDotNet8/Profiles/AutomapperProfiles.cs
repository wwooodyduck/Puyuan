using AutoMapper;
using Microsoft.AspNetCore.Routing.Constraints;
using PuyuanDotNet8.Dtos;
using System.Collections;

namespace PuyuanDotNet8.Profiles
{
    public class AutomapperProfiles : Profile
    {
        public AutomapperProfiles() 
        {
            CreateMap<RegisterDto, UserProfile>()
                .ForMember(x => x.Created_At, y => y.MapFrom(o => DateTime.Now))
                .ForMember(x => x.Password, y => y.Ignore());
        }
            
    }
}
