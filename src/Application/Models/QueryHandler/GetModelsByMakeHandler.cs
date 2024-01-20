using Application.Common.Interfaces;
using Application.Models.Queries;
using MediatR;

namespace Application.Models.QueryHandler
{
    public class GetModelsByMakeHandler : IRequestHandler<GetModelsByMake, IEnumerable<Model>>
    {
        private readonly IModelRepository _repository;

        public GetModelsByMakeHandler(IModelRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Model>> Handle(GetModelsByMake request, CancellationToken cancellationToken)
        {
            return await _repository.GetModelByMake(request.Id);
        }
    }
}
