﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMarket.Server.Shared.DTOs
{
    public class ModificationDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int ModelId { get; set; }
    }
}
