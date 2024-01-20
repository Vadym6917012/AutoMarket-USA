using Application.Common.Interfaces;
using Application.Modifications.Queries;
using MediatR;

namespace Application.Modifications.QueryHandler
{
    public class GetModificationsByModelHandler : IRequestHandler<GetModificationsByModel, IEnumerable<Modification>>
    {
        private readonly IModificationRepository _repository;

        public GetModificationsByModelHandler(IModificationRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Modification>> Handle(GetModificationsByModel request, CancellationToken cancellationToken)
        {
            return await _repository.GetModificationByModel(request.Id);
        }
    }
}
