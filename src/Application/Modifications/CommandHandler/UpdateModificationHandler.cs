using Application.Common.Interfaces;
using Application.Modifications.Commands;
using Ardalis.GuardClauses;
using MediatR;

namespace Application.Modifications.CommandHandler
{
    public class UpdateModificationHandler : IRequestHandler<UpdateModification>
    {
        private readonly IModificationRepository _modificationRepository;

        public UpdateModificationHandler(IModificationRepository modificationRepository)
        {
            _modificationRepository = modificationRepository;
        }

        public async Task Handle(UpdateModification request, CancellationToken cancellationToken)
        {
            var entity = await _modificationRepository.GetByIdAsync(request.Id);

            Guard.Against.NotFound(request.Id, "Такої модифікації не було знайдено");

            entity.Name = request.Name;
            entity.ModelId = request.ModelId;

            await _modificationRepository.UpdateAsync(entity);
        }
    }
}
