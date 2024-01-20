using MediatR;

namespace Application.Generations.Commands
{
    public class DeleteGeneration : IRequest
    {
        public int Id { get; set; }
    }
}
