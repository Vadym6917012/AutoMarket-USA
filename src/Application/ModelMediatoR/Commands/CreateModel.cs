using Domain.Entities;
using MediatR;

namespace Application.ModelMediatoR.Commands
{
    public class CreateModel : IRequest<Model>
    {
        public string Name { get; set; }
        public int MakeId { get; set; }
    }
}
