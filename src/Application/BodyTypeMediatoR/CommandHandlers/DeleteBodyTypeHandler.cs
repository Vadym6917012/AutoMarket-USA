using Application.BodyTypeMediatoR.Commands;
using Application.Common.Interfaces;
using Ardalis.GuardClauses;
using Domain.Entities;
using MediatR;

namespace Application.BodyTypeMediatoR.CommandHandlers
{
    public class DeleteBodyTypeHandler : IRequestHandler<DeleteBodyType>
    {
        private readonly IRepository<BodyType> _repository;

        public DeleteBodyTypeHandler(IRepository<BodyType> repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteBodyType request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);

            Guard.Against.NotFound(request.Id, entity);
            
            await _repository.DeleteAsync(entity);
        }
    }
}
