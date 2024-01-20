using MediatR;

namespace Application.Cars.Queries
{
    public class GetRecentCars : IRequest<IEnumerable<Car>>
    {
        public int Count { get; set; }
    }
}
