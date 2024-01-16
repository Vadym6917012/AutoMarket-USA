using Domain.Entities;
using MediatR;

namespace Application.CarMediatoR.Queries
{
    public class GetRecentCars : IRequest<IEnumerable<Car>>
    {
        public int Count { get; set; }
    }
}
