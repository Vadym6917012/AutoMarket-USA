using MediatR;

namespace Application.ProducingCountries.Commands
{
    public class CreateProducingCountry : IRequest<ProducingCountry>
    {
        public string? Name { get; set; }
    }
}
