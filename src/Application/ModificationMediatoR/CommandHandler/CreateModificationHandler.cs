using Application.Common.Interfaces;
using Application.ModificationMediatoR.Commands;
using Domain.Entities;
using MediatR;

namespace Application.ModificationMediatoR.CommandHandler
{
    public class CreateModificationHandler : IRequestHandler<CreateModification, Modification>
    {
        private readonly IModificationRepository _modificationRepository;

        public CreateModificationHandler(IModificationRepository modificationRepository)
        {
            _modificationRepository = modificationRepository;
        }

        public async Task<Modification> Handle(CreateModification request, CancellationToken cancellationToken)
        {
            var entity = new Modification
            {
                Name = request.Name,
                ModelId = request.ModelId,
            };

            return await _modificationRepository.AddAsync(entity);
        }
    }
}
