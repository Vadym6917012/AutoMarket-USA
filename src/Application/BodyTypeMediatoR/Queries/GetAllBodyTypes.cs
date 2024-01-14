using Domain.Entities;
using MediatR;

namespace Application.BodyTypeMediatoR.Queries
{
    public class GetAllBodyTypes : IRequest<IEnumerable<BodyType>>
    {
        public int Id { get; set; }
        public string? Name { get; set;}
    }
}
