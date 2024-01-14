using Domain.Entities;
using MediatR;

namespace Application.BodyTypeMediatoR.Queries
{
    public class GetBodyTypeById : IRequest<BodyType>
    {
        public int Id { get; set; }
    }
}
