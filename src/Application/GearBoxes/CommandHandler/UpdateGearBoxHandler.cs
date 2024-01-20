using Application.Common.Interfaces;
using Application.GearBoxes.Commands;
using Ardalis.GuardClauses;
using MediatR;

namespace Application.GearBoxes.CommandHandler
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
