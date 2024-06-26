﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cars.Queries
{
    public class CheckCarByVin : IRequest<VinLookupResponse>
    {
        public string VIN {  get; set; }
    }
}
