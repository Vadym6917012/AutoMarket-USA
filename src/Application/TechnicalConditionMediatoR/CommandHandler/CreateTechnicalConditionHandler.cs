using Application.Common.Interfaces;
using Application.TechnicalConditionMediatoR.Commands;
using Domain.Entities;
using MediatR;

namespace Application.TechnicalConditionMediatoR.CommandHandler
{
    public class CreateTechnicalConditionHandler : IRequestHandler<CreateTechnicalCondition, TechnicalCondition>
    {
        private readonly IRepository<TechnicalCondition> _repository;

        public CreateTechnicalConditionHandler(IRepository<TechnicalCondition> repository)
        {
            _repository = repository;
        }

        public async Task<TechnicalCondition> Handle(CreateTechnicalCondition request, CancellationToken cancellationToken)
        {
            var entity = new TechnicalCondition
            {
                Name = request.Name,
                Description = request.Description,
            };

            return await _repository.AddAsync(entity);
        }
    }
}
