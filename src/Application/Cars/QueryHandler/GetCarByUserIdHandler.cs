using Application.Cars.Queries;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Cars.QueryHandler
{
    public class GetCarByUserIdHandler : IRequestHandler<GetCarByUserId, IEnumerable<Car>>
    {
        private readonly ICarRepository _carRepository;

        public GetCarByUserIdHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<IEnumerable<Car>> Handle(GetCarByUserId request, CancellationToken cancellationToken)
        {
            return await _carRepository.GetCarByUserId(request.UserId);
        }
    }
}
