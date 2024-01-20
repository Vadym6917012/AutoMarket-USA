using MediatR;

namespace Application.Makes.Queries
{
    public class GetMakeByProducingCountry : IRequest<IEnumerable<Make>>
    {
        public int Id { get; set; }
    }
}
