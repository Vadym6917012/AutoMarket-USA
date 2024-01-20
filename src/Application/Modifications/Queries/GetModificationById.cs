using MediatR;

namespace Application.Modifications.Queries
{
    public class GetModificationById : IRequest<Modification>
    {
        public int Id { get; set; }
    }
}
