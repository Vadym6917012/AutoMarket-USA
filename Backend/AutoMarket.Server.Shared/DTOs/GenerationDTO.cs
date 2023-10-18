using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMarket.Server.Shared.DTOs
{
    public class GenerationDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int YearFrom { get; set; }
        public int YearTo { get; set; }
    }
}
