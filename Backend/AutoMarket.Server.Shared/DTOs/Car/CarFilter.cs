namespace AutoMarket.Server.Shared.DTOs.Car
{
    public class CarFilter
    {
        public int? MakeId { get; set; }
        public int? ModelId { get; set; }
        public int? GenerationId { get; set; }
        public int? ModificationId { get; set; }
        public int? BodyTypeId { get; set; }
        public int? GearBoxTypeId { get; set; }
        public int? DriveTrainId { get; set; }
        public int? TechnicalConditionId { get; set; }
        public int? FuelTypeId { get; set; }
        public int? YearFrom { get; set; }
        public int? YearTo { get; set; }
        public int? MileageFrom { get; set; }
        public int? MileageTo { get; set; }
        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }
    }
}
