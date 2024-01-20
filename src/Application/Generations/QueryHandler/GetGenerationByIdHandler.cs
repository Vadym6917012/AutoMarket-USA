using Application.Common.Interfaces;
using Application.Generations.Queries;
using Ardalis.GuardClauses;
using MediatR;

namespace Application.Generations.QueryHandler
{
    public class GetGenerationByIdHandler : IRequestHandler<GetGenerationById, Generation>
    {
        private readonly IGenerationRepository _generationRepository;

        public GetGenerationByIdHandler(IGenerationRepository generationRepository)
        {
            _generationRepository = generationRepository;
        }

        public async Task<Generation> Handle(GetGenerationById request, CancellationToken cancellationToken)
        {
            var entity = await _generationRepository.GetByIdAsync(request.Id);

            Guard.Against.NotFound(request.Id, entity);

            return entity;
        }
    }
}
