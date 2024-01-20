using MediatR;

namespace Application.Models.Queries
{
    public class GetModelsByMake : IRequest<IEnumerable<Model>>
    {
        public int Id { get; set; }
    }
}
