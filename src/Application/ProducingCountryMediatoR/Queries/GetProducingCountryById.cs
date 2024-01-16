using Domain.Entities;
using MediatR;

namespace Application.ProducingCountryMediatoR.Queries
{
    public class GetProducingCountryById : IRequest<ProducingCountry>
    {
        public int Id { get; set; }
    }
}
