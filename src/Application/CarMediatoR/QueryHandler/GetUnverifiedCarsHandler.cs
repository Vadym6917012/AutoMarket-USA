using Application.CarMediatoR.Queries;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.CarMediatoR.QueryHandler
{
    public class GetUnverifiedCarsHandler : IRequestHandler<GetUnverifiedCars, IEnumerable<Car>>
    {
        private readonly ICarRepository _carRepository;

        public GetUnverifiedCarsHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<IEnumerable<Car>> Handle(GetUnverifiedCars request, CancellationToken cancellationToken)
        {
            return await _carRepository.GetNotVerifiedCarsAsync();
        }
    }
}
