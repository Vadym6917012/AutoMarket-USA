using Domain.Entities;
using MediatR;

namespace Application.MakeMediatoR.Queries
{
    public class GetAllMakes : IRequest<IEnumerable<Make>>
    {
    }
}
