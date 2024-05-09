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
            SourceMemberNamingConvention = new LowerUnderscoreNamingConvention();
            DestinationMemberNamingConvention = new PascalCaseNamingConvention();
            CreateMap<RegisterDto, UserProfile>()
                .ForMember(x => x.Created_At, y => y.MapFrom(o => DateTime.Now))
                .ForMember(x => x.password, y => y.Ignore());
            //CreateMap<UsersetDto, UserProfile>();
            //CreateMap<UsersetDto, UserSet>();
            CreateMap<UserDefaultDto, Default>();
            CreateMap<SettingDto, Setting>();
            CreateMap<MedicalDto, MedicalInformation>();
        }
    }
}
