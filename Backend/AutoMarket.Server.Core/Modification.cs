using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoMarket.Server.Core
{
    public class Modification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Name { get; set; }
        public int ModelId {  get; set; }
        public Model? Model { get; set; }

        public virtual ICollection<Car>? Cars { get; set; }
    }
}
