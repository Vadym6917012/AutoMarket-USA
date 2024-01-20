using Application.Common.Interfaces;
using Application.FuelTypes.Commands;
using Ardalis.GuardClauses;
using MediatR;

namespace Application.FuelTypes.CommandHandler
{
    public class DeleteFuelTypeHandler : IRequestHandler<DeleteFuelType>
    {
        private readonly IRepository<FuelType> _repository;

        public DeleteFuelTypeHandler(IRepository<FuelType> repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteFuelType request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);

            Guard.Against.NotFound(request.Id, entity);

            await _repository.DeleteAsync(entity);
        }
    }
}
