using Domain.Entities;
using MediatR;

namespace Application.ModificationMediatoR.Queries
{
    public class GetAllModifications : IRequest<IEnumerable<Modification>>
    {
    }
}
