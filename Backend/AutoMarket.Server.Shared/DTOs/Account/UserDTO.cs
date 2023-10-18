using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMarket.Server.Shared.DTOs.Account
{
    public class UserDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set;}
        public string JWT { get; set; }
    }
}
