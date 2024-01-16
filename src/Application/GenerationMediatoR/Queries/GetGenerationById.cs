using Domain.Entities;
using MediatR;

namespace Application.GenerationMediatoR.Queries
{
    public class GetGenerationById : IRequest<Generation>
    {
        public int Id { get; set; }
    }
}
