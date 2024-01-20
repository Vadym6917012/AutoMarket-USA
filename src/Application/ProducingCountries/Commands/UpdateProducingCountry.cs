using MediatR;

namespace Application.ProducingCountries.Commands
{
    public class UpdateProducingCountry : IRequest
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
