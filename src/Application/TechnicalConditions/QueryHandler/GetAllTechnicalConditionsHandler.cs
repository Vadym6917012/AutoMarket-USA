using Application.Common.Interfaces;
using Application.TechnicalConditions.Queries;
using MediatR;

namespace Application.TechnicalConditions.QueryHandler
{
    public class GetAllTechnicalConditionsHandler : IRequestHandler<GetAllTechnicalConditions, IEnumerable<TechnicalCondition>>
    {
        private readonly IRepository<TechnicalCondition> _repository;

        public GetAllTechnicalConditionsHandler(IRepository<TechnicalCondition> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TechnicalCondition>> Handle(GetAllTechnicalConditions request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync();
        }
    }
}
