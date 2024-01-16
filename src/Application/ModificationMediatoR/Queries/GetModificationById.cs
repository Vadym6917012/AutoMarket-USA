using Domain.Entities;
using MediatR;

namespace Application.ModificationMediatoR.Queries
{
    public class GetModificationById : IRequest<Modification>
    {
        public int Id { get; set; }
    }
}
