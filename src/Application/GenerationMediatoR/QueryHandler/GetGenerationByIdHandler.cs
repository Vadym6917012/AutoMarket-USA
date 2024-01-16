using Application.Common.Interfaces;
using Application.GenerationMediatoR.Queries;
using Ardalis.GuardClauses;
using Domain.Entities;
using MediatR;

namespace Application.GenerationMediatoR.QueryHandler
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
