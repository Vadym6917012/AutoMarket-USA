using System.ComponentModel.DataAnnotations;

namespace AutoMarket.Server.Shared.DTOs.Account
{
    public class RegisterDTO
    {
        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Імя повинне бути від {2} до {1} символів")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Прізвище повинне бути від {2} до {1} символів")]
        public string LastName { get; set; }
        [Required]
        [RegularExpression("^\\w+@[a-zA-Z_]+?\\.[a-zA-Z]{2,3}$", ErrorMessage = "Введіть правильний Email")]
        public string Email { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 6, ErrorMessage = "Пароль повинне бути від {2} до {1} символів")]
        public string Password { get; set; }
    }
}
