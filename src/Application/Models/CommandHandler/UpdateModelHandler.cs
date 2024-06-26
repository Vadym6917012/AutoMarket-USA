﻿using Application.Common.Interfaces;
using Application.Models.Commands;
using Ardalis.GuardClauses;
using MediatR;

namespace Application.Models.CommandHandler
{
    public class UpdateModelHandler : IRequestHandler<UpdateModel, Model>
    {
        private readonly IModelRepository _repository;

        public UpdateModelHandler(IModelRepository repository)
        {
            _repository = repository;
        }

        public async Task<Model> Handle(UpdateModel request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);

            Guard.Against.NotFound(request.Id, "Модель, яку ви хочете видалити не знайдена");

            entity.Id = request.Id;
            entity.Name = request.Name;
            entity.MakeId = request.MakeId;

            return await _repository.UpdateAsync(entity);
        }
    }
}
