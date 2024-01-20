using Application.Cars.Queries;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Cars.QueryHandler
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
