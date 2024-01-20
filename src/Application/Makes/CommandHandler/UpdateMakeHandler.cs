using Application.Common.Interfaces;
using Application.Makes.Commands;
using Ardalis.GuardClauses;
using MediatR;

namespace Application.Makes.CommandHandler
{
    public class UpdateMakeHandler : IRequestHandler<UpdateMake, Make>
    {
        private readonly IMakeRepository _repository;

        public UpdateMakeHandler(IMakeRepository repository)
        {
            _repository = repository;
        }

        public async Task<Make> Handle(UpdateMake request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);

            Guard.Against.NotFound(request.Id, entity);

            entity.Name = request.Name;
            entity.ImagePath = request.ImagePath;
            entity.ProducingCountryId = request.ProducingCountryId;

            return await _repository.UpdateAsync(entity);
        }
    }
}
