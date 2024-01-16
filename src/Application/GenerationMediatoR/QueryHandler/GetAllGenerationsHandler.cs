using Application.Common.Interfaces;
using Application.GenerationMediatoR.Queries;
using Domain.Entities;
using MediatR;

namespace Application.GenerationMediatoR.QueryHandler
{
    public class GetAllGenerationsHandler : IRequestHandler<GetAllGenerations, IEnumerable<Generation>>
    {
        private readonly IGenerationRepository _generationRepository;

        public GetAllGenerationsHandler(IGenerationRepository generationRepository)
        {
            _generationRepository = generationRepository;
        }

        public async Task<IEnumerable<Generation>> Handle(GetAllGenerations request, CancellationToken cancellationToken)
        {
            return await _generationRepository.GetAllAsync();
        }
    }
}
