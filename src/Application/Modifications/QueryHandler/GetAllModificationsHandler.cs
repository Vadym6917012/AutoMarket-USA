using Application.Common.Interfaces;
using Application.Modifications.Queries;
using MediatR;

namespace Application.Modifications.QueryHandler
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
