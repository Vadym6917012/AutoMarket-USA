using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMarket.Server.Shared.DTOs.Account
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Email обов`язковий")]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
