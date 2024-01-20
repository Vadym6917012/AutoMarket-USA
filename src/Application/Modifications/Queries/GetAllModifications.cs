using MediatR;

namespace Application.Modifications.Queries
{
    public class GetAllModifications : IRequest<IEnumerable<Modification>>
    {
    }
}
