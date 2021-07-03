using System.Collections.Generic;

namespace ApiRest.WebApi.Models.DTOs
{
    public class UserLoginResponseDto
    {
        public string Token { get; set; }
        public bool Login { get; set; }
        public List<string> Errors { get; set; }
    }
}
