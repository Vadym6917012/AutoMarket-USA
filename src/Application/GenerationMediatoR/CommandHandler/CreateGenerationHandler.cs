﻿using Application.Common.Interfaces;
using Application.GenerationMediatoR.Commands;
using Domain.Entities;
using MediatR;

namespace Application.GenerationMediatoR.CommandHandler
{
    public class CreateGenerationHandler : IRequestHandler<CreateGeneration>
    {
        private readonly IGenerationRepository _generationRepository;

        public CreateGenerationHandler(IGenerationRepository generationRepository)
        {
            _generationRepository = generationRepository;
        }

        public async Task Handle(CreateGeneration request, CancellationToken cancellationToken)
        {
            var entity = new Generation()
            {
                Name = request.Name,
                YearFrom = request.YearFrom,
                YearTo = request.YearTo,
            };

            await _generationRepository.AddToModelAsync(request.ModelId, entity);
        }
    }
}
