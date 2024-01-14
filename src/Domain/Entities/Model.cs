using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
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
