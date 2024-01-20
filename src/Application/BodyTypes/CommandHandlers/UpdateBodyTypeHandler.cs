using Application.BodyTypes.Commands;
using Application.Common.Interfaces;
using Ardalis.GuardClauses;
using MediatR;

namespace Application.BodyTypes.CommandHandlers
{
    public class UpdateBodyTypeHandler : IRequestHandler<UpdateBodyType>
    {
        private readonly IRepository<BodyType> _repository;

        public UpdateBodyTypeHandler(IRepository<BodyType> repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateBodyType request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);

            Guard.Against.NotFound(request.Id, entity);

            entity.Name = request.Name;

            await _repository.UpdateAsync(entity);
        }
    }
}
