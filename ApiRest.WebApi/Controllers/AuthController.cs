using ApiRest.WebApi.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        

        public AuthController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequestDto dto)
        {
            if (ModelState.IsValid)
            {
                var exists = await _userManager.FindByEmailAsync(dto.Email);
                if (exists != null) return BadRequest("Account already exists");

                var isCreated = await _userManager.CreateAsync(new IdentityUser()
                {
                    Email = dto.Email,
                    UserName = dto.Email
                }, dto.Password);

                if (isCreated.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(isCreated.Errors.Select(x => x.Description).ToList());
                }
            }
            else
            {
                return BadRequest("Invalid properties values");
            }
        }

        //[HttpPost]
        //[Route("Login")]
        //public async Task<IActionResult>
    }
}
