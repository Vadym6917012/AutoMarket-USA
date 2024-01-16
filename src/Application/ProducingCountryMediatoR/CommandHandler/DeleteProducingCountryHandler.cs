using Application.Common.Interfaces;
using Application.ProducingCountryMediatoR.Commands;
using Ardalis.GuardClauses;
using Domain.Entities;
using MediatR;

namespace Application.ProducingCountryMediatoR.CommandHandler
{
    public class DeleteProducingCountryHandler : IRequestHandler<DeleteProducingCountry>
    {
        private readonly IRepository<ProducingCountry> _repository;

        public DeleteProducingCountryHandler(IRepository<ProducingCountry> repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteProducingCountry request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);

            Guard.Against.NotFound(request.Id, "Такої країни не було знайдено");

            await _repository.DeleteAsync(entity);
        }
    }
}
