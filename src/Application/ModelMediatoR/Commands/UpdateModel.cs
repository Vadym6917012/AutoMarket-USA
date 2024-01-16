using Domain.Entities;
using MediatR;

namespace Application.ModelMediatoR.Commands
{
    public class UpdateModel : IRequest<Model>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MakeId { get; set; }
    }
}
