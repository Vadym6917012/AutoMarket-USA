using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Car
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ModelId { get; set; }
        public int GenerationId { get; set; }
        public int ModificationId { get; set; }
        public string? VIN { get; set; }
        public int BodyTypeId { get; set; }
        public int GearBoxTypeId { get; set; }
        public int DriveTrainId { get; set; }
        public int FuelTypeId { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public int Mileage { get; set; }
        public string? Description { get; set; }
        public int TechnicalConditionId { get; set; }
        public string? UserId { get; set; }
        public bool IsAdvertisementApproved { get; set; } = false;
        public DateTime? DateCreated { get; set; } = DateTime.UtcNow;

        public Model? Model { get; set; }
        public Generation? Generation { get; set; }
        public Modification? Modification { get; set; }
        public BodyType? BodyType { get; set; }
        public GearBoxType? GearBoxType { get; set; }
        public DriveTrain? DriveTrain { get; set; }
        public FuelType? FuelType { get; set; }
        public TechnicalCondition? TechnicalCondition { get; set; }
        public ApplicationUser? User { get; set; }
        public virtual ICollection<Images>? Images { get; set; }
    }
}
