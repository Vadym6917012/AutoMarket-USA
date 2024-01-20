using Application.Common.Interfaces;
using Application.Models.Commands;
using Ardalis.GuardClauses;
using MediatR;

namespace Application.Models.CommandHandler
{
    public class DeleteModelHandler : IRequestHandler<DeleteModel>
    {
        private readonly IModelRepository _repository;

        public DeleteModelHandler(IModelRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteModel request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);

            Guard.Against.NotFound(request.Id, entity);

            await _repository.DeleteAsync(entity);
        }
    }
}
