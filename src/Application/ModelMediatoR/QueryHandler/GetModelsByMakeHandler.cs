using Application.Common.Interfaces;
using Application.ModelMediatoR.Queries;
using Domain.Entities;
using MediatR;

namespace Application.ModelMediatoR.QueryHandler
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
