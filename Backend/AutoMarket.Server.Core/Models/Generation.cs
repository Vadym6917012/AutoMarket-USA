using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoMarket.Server.Core.Models
{
    public class Generation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Name { get; set; }
        public int YearFrom { get; set; }
        public int YearTo { get; set; }

        public virtual ICollection<Car>? Cars { get; set; }
        public virtual ICollection<ModelGeneration>? ModelGenerations { get; set; }
    }
}