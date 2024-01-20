using Application.Common.Interfaces;
using Application.TechnicalConditions.Commands;
using Ardalis.GuardClauses;
using MediatR;

namespace Application.TechnicalConditions.CommandHandler
{
    public class DeleteTechnicalConditionHandler : IRequestHandler<DeleteTechnicalCondition>
    {
        private readonly IRepository<TechnicalCondition> _repository;

        public DeleteTechnicalConditionHandler(IRepository<TechnicalCondition> repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteTechnicalCondition request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);

            Guard.Against.NotFound(request.Id, "Такої технічної спеціфікації не було знайдено");

            await _repository.DeleteAsync(entity);
        }
    }
}
