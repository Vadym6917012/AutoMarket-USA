using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Car
{
    public class VinCheckResponse
    {
        public bool IsValid { get; set; }
        public string Message { get; set; }
    }
}
