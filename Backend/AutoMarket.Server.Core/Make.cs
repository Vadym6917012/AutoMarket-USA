using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoMarket.Server.Core
{
    public class Make
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Name { get; set; }
        public int ProducingCountryId { get; set; }

        public ProducingCountry? ProducingCountry { get; set; }
        public virtual ICollection<Model>? Models { get; set; }
    }
}
