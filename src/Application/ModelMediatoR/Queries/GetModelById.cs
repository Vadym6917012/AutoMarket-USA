using Domain.Entities;
using MediatR;

namespace Application.ModelMediatoR.Queries
{
    public class GetModelById : IRequest<Model>
    {
        public int Id { get; set; }
    }
}
