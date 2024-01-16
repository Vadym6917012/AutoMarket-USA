using MediatR;

namespace Application.GenerationMediatoR.Commands
{
    public class CreateGeneration : IRequest
    {
        public int ModelId { get; set; }
        public string Name { get; set; }
        public int YearFrom { get; set; }
        public int YearTo { get; set; }
    }
}
