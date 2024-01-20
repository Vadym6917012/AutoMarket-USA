using Application.Cars.Queries;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Cars.QueryHandler
{
    public class GetCarByIdHandler : IRequestHandler<GetCarById, Car>
    {
        private readonly ICarRepository _carRepository;

        public GetCarByIdHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<Car> Handle(GetCarById request, CancellationToken cancellationToken)
        {
            return await _carRepository.GetByIdAsync(request.Id);
        }
    }
}
