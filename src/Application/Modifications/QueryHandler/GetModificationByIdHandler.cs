using Application.Common.Interfaces;
using Application.Modifications.Queries;
using Ardalis.GuardClauses;
using MediatR;

namespace Application.Modifications.QueryHandler
{
    public class GetModificationByIdHandler : IRequestHandler<GetModificationById, Modification>
    {
        private readonly IModificationRepository _modificationRepository;

        public GetModificationByIdHandler(IModificationRepository modificationRepository)
        {
            _modificationRepository = modificationRepository;
        }

        public async Task<Modification> Handle(GetModificationById request, CancellationToken cancellationToken)
        {
            var entity = await _modificationRepository.GetByIdAsync(request.Id);

            Guard.Against.NotFound(request.Id, "Такої модифікації знайдено не було");

            return entity;
        }
    }
}
