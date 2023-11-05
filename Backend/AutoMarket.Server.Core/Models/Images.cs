using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoMarket.Server.Core.Models
{
    public class Images
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? ImagePath { get; set; }
        public string? ImagePathToDisplay { get; set; }
        public int CarId { get; set; }
        public Car? Car { get; set; }
    }
}
