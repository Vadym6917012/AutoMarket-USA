using Domain.Entities;
using MediatR;

namespace Application.ModelMediatoR.Queries
{
    public class GetModelsByMake : IRequest<IEnumerable<Model>>
    {
        public int Id { get; set; }
    }
}
