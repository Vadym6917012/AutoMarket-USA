using Application.Common.Interfaces;
using Application.ModificationMediatoR.Queries;
using Ardalis.GuardClauses;
using Domain.Entities;
using MediatR;

namespace Application.ModificationMediatoR.QueryHandler
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
