using Application.Common.Interfaces;
using Application.Modifications.Commands;
using Ardalis.GuardClauses;
using MediatR;

namespace Application.Modifications.CommandHandler
{
    public class DeleteModificationHandler : IRequestHandler<DeleteModification>
    {
        private readonly IModificationRepository _modificationRepository;

        public DeleteModificationHandler(IModificationRepository modificationRepository)
        {
            _modificationRepository = modificationRepository;
        }

        public async Task Handle(DeleteModification request, CancellationToken cancellationToken)
        {
            var entity = await _modificationRepository.GetByIdAsync(request.Id);

            Guard.Against.NotFound(request.Id, "Такої модифікації не було знайдено");

            await _modificationRepository.DeleteAsync(entity);
        }
    }
}
