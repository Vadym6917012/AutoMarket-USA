using MediatR;

namespace Application.ProducingCountryMediatoR.Commands
{
    public class DeleteProducingCountry : IRequest
    {
        public int Id { get; set; }
    }
}
