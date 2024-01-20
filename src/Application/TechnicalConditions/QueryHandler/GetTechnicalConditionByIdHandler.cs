using Application.Common.Interfaces;
using Application.TechnicalConditions.Queries;
using Ardalis.GuardClauses;
using MediatR;

namespace Application.TechnicalConditions.QueryHandler
{
    public class GetTechnicalConditionByIdHandler : IRequestHandler<GetTechnicalConditionById, TechnicalCondition>
    {
        private readonly IRepository<TechnicalCondition> _repository;

        public GetTechnicalConditionByIdHandler(IRepository<TechnicalCondition> repository)
        {
            _repository = repository;
        }

        public async Task<TechnicalCondition> Handle(GetTechnicalConditionById request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);

            Guard.Against.NotFound(request.Id, "Такої технічної специфікації не було знайдено");

            return entity;
        }
    }
}
