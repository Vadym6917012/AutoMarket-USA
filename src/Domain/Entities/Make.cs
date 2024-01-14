using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Make
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Name { get; set; }

        //ImageToDisplay https://localhost:7119/Make icons\{make.jpg}
        public string? ImagePath { get; set; }
        public int ProducingCountryId { get; set; }

        public ProducingCountry? ProducingCountry { get; set; }
        public virtual ICollection<Model>? Models { get; set; }
    }
}
