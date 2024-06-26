﻿using Application.Common.Interfaces;
using Application.TechnicalConditions.Commands;
using Ardalis.GuardClauses;
using MediatR;

namespace Application.TechnicalConditions.CommandHandler
{
    public class UpdateTechnicalConditionHandler : IRequestHandler<UpdateTechnicalCondition>
    {
        private readonly IRepository<TechnicalCondition> _repository;

        public UpdateTechnicalConditionHandler(IRepository<TechnicalCondition> repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateTechnicalCondition request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);

            Guard.Against.NotFound(request.Id, "Такої технічної спеціфікації не було знайдено");

            entity.Name = request.Name;
            entity.Description = request.Description;

            await _repository.UpdateAsync(entity);
        }
    }
}
