using Application.Common.Interfaces;
using Application.Models.Queries;
using MediatR;

namespace Application.Models.QueryHandler
{
    public class GetAllModelsHandler : IRequestHandler<GetAllModels, IEnumerable<Model>>
    {
        private readonly IModelRepository _repository;

        public GetAllModelsHandler(IModelRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Model>> Handle(GetAllModels request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync();
        }
    }
}
