using Application.Common.Interfaces;
using Application.Generations.Queries;
using MediatR;

namespace Application.Generations.QueryHandler
{
    public class GetGenerationByModelHandler : IRequestHandler<GetGenerationsByModel, IEnumerable<Generation>>
    {
        private readonly IGenerationRepository _repository;

        public GetGenerationByModelHandler(IGenerationRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Generation>> Handle(GetGenerationsByModel request, CancellationToken cancellationToken)
        {
            return await _repository.GetGenerationsByModel(request.Id);
        }
    }
}
