using Application.Cars.Queries;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Cars.QueryHandler
{
    public class CheckCarByVinHandler : IRequestHandler<CheckCarByVin, bool>
    {
        private readonly ICarRepository _carRepository;

        public CheckCarByVinHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<bool> Handle(CheckCarByVin request, CancellationToken cancellationToken)
        {
            return await _carRepository.CheckVin(request.VIN);
        }
    }
}
