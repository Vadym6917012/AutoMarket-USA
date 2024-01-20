using Application.Cars.Queries;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Cars.QueryHandler
{
    public class GetRecentCarsHandler : IRequestHandler<GetRecentCars, IEnumerable<Car>>
    {
        private readonly ICarRepository _carRepository;

        public GetRecentCarsHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<IEnumerable<Car>> Handle(GetRecentCars request, CancellationToken cancellationToken)
        {
            return await _carRepository.GetRecentCars(request.Count);
        }
    }
}
