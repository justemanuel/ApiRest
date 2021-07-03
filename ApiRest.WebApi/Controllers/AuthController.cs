using ApiRest.Services;
using ApiRest.WebApi.Config;
using ApiRest.WebApi.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        private readonly ITokenHandleService _tokenService;

        public AuthController(UserManager<IdentityUser> userManager, ITokenHandleService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
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

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequestDto dto)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(dto.UserName);
                if (existingUser == null)
                {
                    return BadRequest(new UserLoginResponseDto
                    {
                        Login = false,
                        Errors = new List<string>()
                        {
                            "Incorrect username"
                        }
                    });
                }

                var isCorrect = await _userManager.CheckPasswordAsync(existingUser, dto.Password);
                if (isCorrect)
                {
                    var pars = new TokenParameters()
                    {
                        Id = existingUser.Id,
                        Passwordhash = existingUser.PasswordHash,
                        UserName = existingUser.UserName
                    };

                    var token = _tokenService.GenerateJwtToken(pars);

                    return Ok(new UserLoginResponseDto
                    {
                        Token = token,
                        Login = true
                    });
                }
                else
                {
                    return BadRequest(new UserLoginResponseDto
                    {
                        Login = false,
                        Errors = new List<string>()
                        {
                            "Incorrect password"
                        }
                    });
                }

            }
            else
            {
                return BadRequest("Invalid properties values");
            }
        }
    }
}
