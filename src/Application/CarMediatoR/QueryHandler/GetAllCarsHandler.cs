using Application.CarMediatoR.Queries;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.CarMediatoR.QueryHandler
{
    public class GetAllCarsHandler : IRequestHandler<GetAllCars, IEnumerable<Car>>
    {
        private readonly ICarRepository _carRepository;

        public GetAllCarsHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<IEnumerable<Car>> Handle(GetAllCars request, CancellationToken cancellationToken)
        {
            return await _carRepository.GetAllAsync();
        }
    }
}
