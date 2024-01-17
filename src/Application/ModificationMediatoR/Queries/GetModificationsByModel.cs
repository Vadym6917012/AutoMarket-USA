using Domain.Entities;
using MediatR;

namespace Application.ModificationMediatoR.Queries
{
    public class GetModificationsByModel : IRequest<IEnumerable<Modification>>
    {
        public int Id { get; set; }
    }
}
