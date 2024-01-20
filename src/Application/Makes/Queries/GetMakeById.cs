using MediatR;

namespace Application.Makes.Queries
{
    public class GetMakeById : IRequest<Make>
    {
        public int Id { get; set; }
    }
}
