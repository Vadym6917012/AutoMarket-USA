using MediatR;

namespace Application.ProducingCountries.Commands
{
    public class DeleteProducingCountry : IRequest
    {
        public int Id { get; set; }
    }
}
