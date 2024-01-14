namespace Application.DTOs.Car
{
    public class CarHomeFilter
    {
        public int? MakeId { get; set; }
        public int? ModelId { get; set; }
        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }
    }
}
