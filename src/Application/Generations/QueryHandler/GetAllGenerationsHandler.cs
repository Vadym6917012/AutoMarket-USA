using Application.Common.Interfaces;
using Application.Generations.Queries;
using MediatR;

namespace Application.Generations.QueryHandler
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
