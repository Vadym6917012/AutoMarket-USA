using MediatR;

namespace Application.Generations.Queries
{
    public class GetGenerationById : IRequest<Generation>
    {
        public int Id { get; set; }
    }
}
