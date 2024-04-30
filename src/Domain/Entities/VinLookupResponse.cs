using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class VinLookupResponse
    {
        public string Vin {  get; set; }
        public string Country { get; set; }
        public string Manufacturer { get; set; }
        public string[] Years { get; set; }
    }
}
