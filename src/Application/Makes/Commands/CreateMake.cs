using MediatR;

namespace Application.Makes.Commands
{
    public class CreateMake : IRequest<Make>
    {
        public string? Name { get; set; }
        public string? ImagePath { get; set; }
        public int ProducingCountryId { get; set; }
        public List<int>? ModelsId { get; set; }
    }
}
