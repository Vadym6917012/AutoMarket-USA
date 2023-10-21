using System.ComponentModel.DataAnnotations;

namespace AutoMarket.Server.Shared.DTOs.Account
{
    public class ConfirmEmailDTO
    {
        [Required]
        public string Token { get; set; }
        [Required]
        [RegularExpression("^\\w+@[a-zA-Z_]+?\\.[a-zA-Z]{2,3}$", ErrorMessage = "Введіть правильний Email")]
        public string Email { get; set; }
    }
}
