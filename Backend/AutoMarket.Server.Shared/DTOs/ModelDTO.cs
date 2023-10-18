using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMarket.Server.Shared.DTOs
{
    public class ModelDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int MakeId { get; set; }
        public ICollection<string>? Generations { get; set; }
    }
}
