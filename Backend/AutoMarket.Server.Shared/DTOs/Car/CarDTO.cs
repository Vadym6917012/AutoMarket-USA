namespace AutoMarket.Server.Shared.DTOs
{
    public class CarDTO
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string ModificationName { get; set; }
        public string? VIN { get; set; }
        public string BodyTypeName { get; set; }
        public string GearBoxTypeName { get; set; }
        public string FuelTypeName { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public int Mileage { get; set; }
        public string? Description { get; set; }
        public string? UserId { get; set; }
        public List<string>? ImagesPath { get; set; }
    }
}
