using Domain.Entities;
using MediatR;

namespace Application.GenerationMediatoR.Queries
{
    public class GetGenerationsByModel : IRequest<IEnumerable<Generation>>
    {
        public int Id { get; set; }
    }
}
