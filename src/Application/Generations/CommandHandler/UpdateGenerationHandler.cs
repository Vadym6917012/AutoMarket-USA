using Application.Common.Interfaces;
using Application.Generations.Commands;
using Ardalis.GuardClauses;
using MediatR;

namespace Application.Generations.CommandHandler
{
    public class UpdateGenerationHandler : IRequestHandler<UpdateGeneration, Generation>
    {
        private readonly IGenerationRepository _generationRepository;

        public UpdateGenerationHandler(IGenerationRepository generationRepository)
        {
            _generationRepository = generationRepository;
        }

        public async Task<Generation> Handle(UpdateGeneration request, CancellationToken cancellationToken)
        {
            var entity = await _generationRepository.GetByIdAsync(request.Id);

            Guard.Against.NotFound(request.Id, entity);

            entity.Name = request.Name;
            entity.YearFrom = request.YearFrom;
            entity.YearTo = request.YearTo;

            return await _generationRepository.UpdateAsync(entity);
        }
    }
}
