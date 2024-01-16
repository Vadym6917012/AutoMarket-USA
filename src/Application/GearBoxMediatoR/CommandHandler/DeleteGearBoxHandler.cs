using Application.Common.Interfaces;
using Application.GearBoxMediatoR.Commands;
using Ardalis.GuardClauses;
using Domain.Entities;
using MediatR;

namespace Application.GearBoxMediatoR.CommandHandler
{
    public class DeleteGearBoxHandler : IRequestHandler<DeleteGearBox>
    {
        private readonly IRepository<GearBoxType> _repository;

        public DeleteGearBoxHandler(IRepository<GearBoxType> repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteGearBox request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);

            Guard.Against.NotFound(request.Id, entity);

            await _repository.DeleteAsync(entity);
        }
    }
}
