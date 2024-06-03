using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Car
{
    public class VinCheckResponse
    {
        public string Vin { get; set; }
        public string Country { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string Class { get; set; }
        public string Region { get; set; }
        public string Wmi { get; set; }
        public string Vds { get; set; }
        public string Vis { get; set; }
        public int Year { get; set; }
        public string Message { get; set; }
        public bool IsFound { get; set; }
    }
}
