using MediatR;

namespace Application.Models.Queries
{
    public class GetModelById : IRequest<Model>
    {
        public int Id { get; set; }
    }
}
