using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiRest.Entities;
using AutoMapper;

namespace ApiRest.WebApi.Models.Mapping
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<FootballTeamDto, FootballTeam>().ReverseMap();
        }
    }
}
