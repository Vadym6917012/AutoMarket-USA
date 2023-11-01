using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMarket.Server.Shared.DTOs.Admin
{
    public class MemberAddEditDto
    {
        public string Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Password { get; set; }
        [Required]
        public string Roles { get; set; }
    }
}
