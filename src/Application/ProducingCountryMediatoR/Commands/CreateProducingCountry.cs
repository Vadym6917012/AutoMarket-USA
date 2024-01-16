using Domain.Entities;
using MediatR;

namespace Application.ProducingCountryMediatoR.Commands
{
    public class CreateProducingCountry : IRequest<ProducingCountry>
    {
        public string? Name { get; set; }
    }
}
