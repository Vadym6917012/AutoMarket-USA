using MediatR;

namespace Application.BodyTypes.Queries
{
    public class GetAllBodyTypes : IRequest<IEnumerable<BodyType>>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
