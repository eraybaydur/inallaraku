using InallarEticaretWebService.Dtos.User;
using System.ComponentModel.DataAnnotations;

namespace InallarEticaretWebService.Models.Users
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class LoginResponseDto
    {
        public UserDto User { get; set; }
        public string Token { get; set; }
    }
}
