using ApiRest.Application;
using ApiRest.Entities;
using ApiRest.WebApi.Models.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiRest.WebApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class FootballTeamController : ControllerBase
    {
        IApplication<FootballTeam> _appFootball;
        private readonly IMapper _mapper;
        private readonly ILogger<FootballTeamController> _logger;

        public FootballTeamController(IApplication<FootballTeam> appFootball, IMapper mapper,
            ILogger<FootballTeamController> logger)
        {
            _appFootball = appFootball;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("Get teams informations");
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
