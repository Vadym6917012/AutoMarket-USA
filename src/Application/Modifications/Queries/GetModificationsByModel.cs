using MediatR;

namespace Application.Modifications.Queries
{
    public class GetModificationsByModel : IRequest<IEnumerable<Modification>>
    {
        public int Id { get; set; }
    }
}
