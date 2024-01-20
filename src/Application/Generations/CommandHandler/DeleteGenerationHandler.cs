using Application.Common.Interfaces;
using Application.Generations.Commands;
using Ardalis.GuardClauses;
using MediatR;

namespace Application.Generations.CommandHandler
{
    public class DeleteGenerationHandler : IRequestHandler<DeleteGeneration>
    {
        private readonly IGenerationRepository _repository;

        public DeleteGenerationHandler(IGenerationRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteGeneration request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);

            Guard.Against.NotFound(request.Id, entity);

            await _repository.DeleteAsync(entity);
        }
    }
}
