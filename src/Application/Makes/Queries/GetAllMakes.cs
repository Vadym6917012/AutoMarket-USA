using MediatR;

namespace Application.Makes.Queries
{
    public class GetAllMakes : IRequest<IEnumerable<Make>>
    {
    }
}
