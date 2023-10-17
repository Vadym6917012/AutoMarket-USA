using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMarket.Server.Core
{
    public class Model
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Name { get; set; }
        public int MakeId { get; set; }

        public Make? Make { get; set; }
        public virtual ICollection<Modification>? Modifications { get; set; }
        public virtual ICollection<ModelGeneration>? ModelGenerations { get; set; }
        public virtual ICollection<Car>? Cars { get; set; }
    }
}
