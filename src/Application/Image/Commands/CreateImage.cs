using MediatR;

namespace Application.Image.Commands
{
    public class CreateImage : IRequest
    {
        public string? ImagePath { get; set; }
        public string? ImagePathToDisplay { get; set; }
        public int CarId { get; set; }
        public Car? Car { get; set; }
    }
}
