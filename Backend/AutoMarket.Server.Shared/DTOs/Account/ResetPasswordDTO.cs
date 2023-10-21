using System.ComponentModel.DataAnnotations;

namespace AutoMarket.Server.Shared.DTOs.Account
{
    public class ResetPasswordDTO
    {
        [Required]
        public string Token { get; set; }
        [Required]
        [RegularExpression("^\\w+@[a-zA-Z_]+?\\.[a-zA-Z]{2,3}$", ErrorMessage = "Введіть правильний Email")]
        public string Email { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 6, ErrorMessage = "Пароль повинне бути від {2} до {1} символів")]
        public string NewPassword { get; set; }
    }
}
