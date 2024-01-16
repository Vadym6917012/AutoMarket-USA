using Application.Common.Interfaces;
using Application.TechnicalConditionMediatoR.Queries;
using Domain.Entities;
using MediatR;

namespace Application.TechnicalConditionMediatoR.QueryHandler
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
