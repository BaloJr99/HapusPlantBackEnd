using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HapusPlant.Data.DTO;
using HapusPlant.Data.Models;

namespace HapusPlant.Bussiness.Automapper
{
    public class AutomapperProfile: Profile
    {
        public AutomapperProfile()
        {
            CreateMap<NewUserDTO, UserDTO>()
            .ForMember(x => x.Username, ( opt => opt.MapFrom(src => src.user.Username)))
            .ForMember(x => x.Password, ( opt => opt.MapFrom(src => src.user.Password)))
            .ForMember(x => x.IsActive, ( opt => opt.MapFrom(src => src.user.IsActive)))
            .ForMember(x => x.Role, ( opt => opt.MapFrom(src => src.user.Role)));
            CreateMap<NewUserDTO, PersonalDatum>()
            .ForMember(x => x.Name, ( opt => opt.MapFrom(src => src.personalDatum.Name)))
            .ForMember(x => x.LastName, ( opt => opt.MapFrom(src => src.personalDatum.LastName)))
            .ForMember(x => x.Birthday, ( opt => opt.MapFrom(src => src.personalDatum.Birthday)))
            .ForMember(x => x.Photo, ( opt => opt.MapFrom(src => src.personalDatum.Photo)));
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<PersonalDatum, PersonalDatumDTO>().ReverseMap();
            CreateMap<SucculentFamily, SucculentFamilyDTO>().ReverseMap();
            CreateMap<SucculentKind, SucculentKindDTO>().ReverseMap();
        }
    }
}