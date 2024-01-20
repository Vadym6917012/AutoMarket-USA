using Application.Common.Interfaces;
using Application.ProducingCountries.Commands;
using MediatR;

namespace Application.ProducingCountries.CommandHandler
{
    public class CreateProducingCountryHandler : IRequestHandler<CreateProducingCountry, ProducingCountry>
    {
        private readonly IRepository<ProducingCountry> _repository;

        public CreateProducingCountryHandler(IRepository<ProducingCountry> repository)
        {
            _repository = repository;
        }

        public async Task<ProducingCountry> Handle(CreateProducingCountry request, CancellationToken cancellationToken)
        {
            var entity = new ProducingCountry
            {
                Name = request.Name,
            };

            return await _repository.AddAsync(entity);
        }
    }
}
