using Application.Common.Interfaces;
using Application.ModificationMediatoR.Queries;
using Domain.Entities;
using MediatR;

namespace Application.ModificationMediatoR.QueryHandler
{
    public class GetAllModificationsHandler : IRequestHandler<GetAllModifications, IEnumerable<Modification>>
    {
        private readonly IModificationRepository _modificationRepository;

        public GetAllModificationsHandler(IModificationRepository modificationRepository)
        {
            _modificationRepository = modificationRepository;
        }

        public async Task<IEnumerable<Modification>> Handle(GetAllModifications request, CancellationToken cancellationToken)
        {
            return await _modificationRepository.GetAllAsync();
        }
    }
}
