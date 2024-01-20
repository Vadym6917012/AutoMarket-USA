using MediatR;

namespace Application.ProducingCountries.Queries
{
    public class GetProducingCountryById : IRequest<ProducingCountry>
    {
        public int Id { get; set; }
    }
}
