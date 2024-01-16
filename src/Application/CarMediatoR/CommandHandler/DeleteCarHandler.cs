using Application.CarMediatoR.Commands;
using Application.Common.Interfaces;
using Ardalis.GuardClauses;
using MediatR;

namespace Application.CarMediatoR.CommandHandler
{
    public class DeleteCarHandler : IRequestHandler<DeleteCar>
    {
        private readonly ICarRepository _carRepository;

        public DeleteCarHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task Handle(DeleteCar request, CancellationToken cancellationToken)
        {
            var entity = await _carRepository.GetByIdAsync(request.Id);

            Guard.Against.NotFound(request.Id, entity);

            await _carRepository.DeleteAsync(entity);
        }
    }
}
