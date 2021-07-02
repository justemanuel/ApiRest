using ApiRest.Application;
using ApiRest.Entities;
using ApiRest.WebApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FootballTeamController : ControllerBase
    {
        IApplication<FootballTeam> _appFootball;
        private readonly IMapper _mapper;

        public FootballTeamController(IApplication<FootballTeam> appFootball, IMapper mapper)
        {
            _appFootball = appFootball;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var teams = _appFootball.GetAll();

            return Ok(_mapper.Map<IEnumerable<FootballTeamDto>>(teams));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var team = _appFootball.GetById(id);

            if (team == null) return NotFound();

            var dto = _mapper.Map<FootballTeam>(team);

            return Ok(dto);
        }

        [HttpPost]
        public IActionResult Post(FootballTeamDto dto)
        {
            var team = _mapper.Map<FootballTeam>(dto);

            _appFootball.Save(team);

            return CreatedAtAction(nameof(Get), new { id = team.Id }, team);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var item = _appFootball.GetById(id);

            if (item == null) return NotFound();

            _appFootball.Delete(id);

            return Ok();
        }
    }
}
