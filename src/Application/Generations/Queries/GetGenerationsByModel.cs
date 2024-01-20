using MediatR;

namespace Application.Generations.Queries
{
    public class GetGenerationsByModel : IRequest<IEnumerable<Generation>>
    {
        public int Id { get; set; }
    }
}
