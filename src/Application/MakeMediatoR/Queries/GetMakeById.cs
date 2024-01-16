using Domain.Entities;
using MediatR;

namespace Application.MakeMediatoR.Queries
{
    public class GetMakeById : IRequest<Make>
    {
        public int Id { get; set; }
    }
}
