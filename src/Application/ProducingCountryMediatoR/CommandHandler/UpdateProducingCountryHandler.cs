using Application.Common.Interfaces;
using Application.ProducingCountryMediatoR.Commands;
using Ardalis.GuardClauses;
using Domain.Entities;
using MediatR;

namespace Application.ProducingCountryMediatoR.CommandHandler
{
    public class UpdateProducingCountryHandler : IRequestHandler<UpdateProducingCountry>
    {
        private readonly IRepository<ProducingCountry> _repository;

        public UpdateProducingCountryHandler(IRepository<ProducingCountry> repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateProducingCountry request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);

            Guard.Against.NotFound(request.Id, "Такої країни не було знайдено");

            entity.Name = request.Name;

            await _repository.UpdateAsync(entity);
        }
    }
}
