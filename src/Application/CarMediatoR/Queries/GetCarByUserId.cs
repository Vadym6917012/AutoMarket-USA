using Domain.Entities;
using MediatR;

namespace Application.CarMediatoR.Queries
{
    public class GetCarByUserId : IRequest<IEnumerable<Car>>
    {
        public string UserId { get; set; }
    }
}
