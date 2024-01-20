using Application.Common.Interfaces;
using Application.Makes.Commands;
using Ardalis.GuardClauses;
using MediatR;

namespace Application.Makes.CommandHandler
{
    public class DeleteMakeHandler : IRequestHandler<DeleteMake>
    {
        private readonly IMakeRepository _repository;

        public DeleteMakeHandler(IMakeRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteMake request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);

            Guard.Against.NotFound(request.Id, entity);

            await _repository.DeleteAsync(entity);
        }
    }
}
