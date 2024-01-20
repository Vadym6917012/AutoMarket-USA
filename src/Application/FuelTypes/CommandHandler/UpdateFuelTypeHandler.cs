using Application.Common.Interfaces;
using Application.FuelTypes.Commands;
using Ardalis.GuardClauses;
using MediatR;

namespace Application.FuelTypes.CommandHandler
{
    public class UpdateFuelTypeHandler : IRequestHandler<UpdateFuelType>
    {
        private readonly IRepository<FuelType> _repository;

        public UpdateFuelTypeHandler(IRepository<FuelType> repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateFuelType request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);

            Guard.Against.NotFound(request.Id, entity);

            entity.Name = request.Name;

            await _repository.UpdateAsync(entity);
        }
    }
}
