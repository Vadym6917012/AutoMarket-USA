namespace Application.DTOs.Images
{
    public class ImagesDTO
    {
        public int Id { get; set; }
        public string? ImagePath { get; set; }
        public string? ImagePathToDisplay { get; set; }
        public int CarId { get; set; }
    }
}
