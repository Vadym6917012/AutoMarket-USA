using Domain.Entities;
using MediatR;

namespace Application.MakeMediatoR.Queries
{
    public class GetMakeByProducingCountry : IRequest<IEnumerable<Make>>
    {
        public int Id { get; set; }
    }
}
