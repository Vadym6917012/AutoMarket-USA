using System.ComponentModel.DataAnnotations;

namespace AutoMarket.Server.Shared.DTOs.Car
{
    public class CarCreateDTO
    {
        public int Id { get; set; }
        public int ModelId { get; set; }
        public int GenerationId { get; set; }
        public int? ModificationId { get; set; }
        [RegularExpression("^[A-HJ-NPR-Z0-9]{17}$")]
        public string? VIN { get; set; }
        public int BodyTypeId { get; set; }
        public int GearBoxTypeId { get; set; }
        public int DriveTrainId { get; set; }
        public int TechnicalConditionId { get; set; }
        public int FuelTypeId { get; set; }
        public int Year { get; set; }
        [RegularExpression("^\\d{4}$")]
        public decimal Price { get; set; }
        public int Mileage { get; set; }
        public string? Description { get; set; }
        public string? UserId { get; set; }
    }
}
