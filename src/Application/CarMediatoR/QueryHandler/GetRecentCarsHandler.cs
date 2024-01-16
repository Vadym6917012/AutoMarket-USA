using Application.CarMediatoR.Queries;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.CarMediatoR.QueryHandler
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
