using Application.Common.Interfaces;
using Application.Models.Queries;
using Ardalis.GuardClauses;
using MediatR;

namespace Application.Models.QueryHandler
{
    public class GetModelByIdHandler : IRequestHandler<GetModelById, Model>
    {
        private readonly IModelRepository _repository;

        public GetModelByIdHandler(IModelRepository repository)
        {
            _repository = repository;
        }

        public async Task<Model> Handle(GetModelById request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);

            Guard.Against.NotFound(request.Id, entity);

            return entity;
        }
    }
}
