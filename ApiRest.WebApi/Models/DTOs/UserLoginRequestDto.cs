using System.ComponentModel.DataAnnotations;

namespace ApiRest.WebApi.Models.DTOs
{
    public class UserLoginRequestDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
