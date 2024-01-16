using MediatR;

namespace Application.GenerationMediatoR.Commands
{
    public class DeleteGeneration : IRequest
    {
        public int Id { get; set; }
    }
}
