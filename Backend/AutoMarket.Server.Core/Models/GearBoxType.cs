﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoMarket.Server.Core.Models
{
    public class GearBoxType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Car>? Cars { get; set; }
    }
}