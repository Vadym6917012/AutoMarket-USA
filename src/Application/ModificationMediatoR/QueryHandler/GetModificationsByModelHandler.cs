using Application.Common.Interfaces;
using Application.ModificationMediatoR.Queries;
using Domain.Entities;
using MediatR;

namespace Application.ModificationMediatoR.QueryHandler
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
