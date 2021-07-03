using ApiRest.Entities;
using ApiRest.WebApi.Models.DTOs;
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
