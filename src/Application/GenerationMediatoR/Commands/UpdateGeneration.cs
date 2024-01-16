using Domain.Entities;
using MediatR;

namespace Application.GenerationMediatoR.Commands
{
    public class UpdateGeneration : IRequest<Generation>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int YearFrom { get; set; }
        public int YearTo { get; set; }
    }
}
