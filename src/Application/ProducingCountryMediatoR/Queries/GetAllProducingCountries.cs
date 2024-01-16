using Domain.Entities;
using MediatR;

namespace Application.ProducingCountryMediatoR.Queries
{
    public class GetAllProducingCountries : IRequest<IEnumerable<ProducingCountry>>
    {

    }
}
