using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Account
{
    public class LoginUserRequest
    {
        [Required(ErrorMessage = "Email обов`язковий")]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
