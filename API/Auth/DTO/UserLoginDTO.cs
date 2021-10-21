using System.ComponentModel.DataAnnotations;

namespace Auth.API.Auth.DTO
{
    public class UserLoginDTO
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
