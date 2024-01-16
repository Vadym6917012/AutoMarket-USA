using Application.Common.Interfaces;
using Application.GearBoxMediatoR.Commands;
using Ardalis.GuardClauses;
using Domain.Entities;
using MediatR;

namespace Application.GearBoxMediatoR.CommandHandler
{
    public class UpdateGearBoxHandler : IRequestHandler<UpdateGearBox>
    {
        private readonly IRepository<GearBoxType> _repository;

        public UpdateGearBoxHandler(IRepository<GearBoxType> repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateGearBox request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);

            Guard.Against.NotFound(request.Id, entity);

            entity.Name = request.Name;

            await _repository.UpdateAsync(entity);
        }
    }
}
