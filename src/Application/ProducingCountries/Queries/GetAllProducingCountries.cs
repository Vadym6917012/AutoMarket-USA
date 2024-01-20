using MediatR;

namespace Application.ProducingCountries.Queries
{
    public class GetAllProducingCountries : IRequest<IEnumerable<ProducingCountry>>
    {

    }
}
