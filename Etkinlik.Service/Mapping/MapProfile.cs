using AutoMapper;
using Etkinlik.Core.DTOs;
using Etkinlik.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etkinlik.Service.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<ActivityDto, Activity>().ReverseMap();
            CreateMap<CompanyDto, Company>().ReverseMap();
            CreateMap<ActivityWithAllDetails, Activity>().ReverseMap();
            CreateMap<AddActivityDto, ActivityDto>().ReverseMap();
            CreateMap<Participant, ParticipantDto>().ReverseMap();
            CreateMap<ActivitiesForSellersDto, Activity>().ReverseMap();
        }
    }
}
