using Application.Common.Interfaces;
using Application.ModelMediatoR.Queries;
using Ardalis.GuardClauses;
using Domain.Entities;
using MediatR;

namespace Application.ModelMediatoR.QueryHandler
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
