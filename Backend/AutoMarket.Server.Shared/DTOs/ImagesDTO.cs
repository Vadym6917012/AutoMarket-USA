namespace AutoMarket.Server.Shared.DTOs
{
    public class ImagesDTO
    {
        public int Id { get; set; }
        public string? ImagePath { get; set; }
        public string? ImagePathToDisplay { get; set; }
        public int CarId { get; set; }
    }
}
