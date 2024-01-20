using Application.Common.Interfaces;
using Application.Makes.Queries;
using Ardalis.GuardClauses;
using MediatR;

namespace Application.Makes.QueryHandler
{
    public class GetMakeByIdHandler : IRequestHandler<GetMakeById, Make>
    {
        private readonly IMakeRepository _repository;

        public GetMakeByIdHandler(IMakeRepository repository)
        {
            _repository = repository;
        }

        public async Task<Make> Handle(GetMakeById request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);

            Guard.Against.NotFound(request.Id, entity);

            return entity;
        }
    }
}
