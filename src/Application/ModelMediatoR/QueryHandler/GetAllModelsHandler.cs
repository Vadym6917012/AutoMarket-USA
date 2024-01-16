using Application.Common.Interfaces;
using Application.ModelMediatoR.Queries;
using Domain.Entities;
using MediatR;

namespace Application.ModelMediatoR.QueryHandler
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
