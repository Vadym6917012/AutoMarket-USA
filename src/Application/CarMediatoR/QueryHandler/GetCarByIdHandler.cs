using Application.CarMediatoR.Queries;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.CarMediatoR.QueryHandler
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
