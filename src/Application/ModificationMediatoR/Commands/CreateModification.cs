using Domain.Entities;
using MediatR;

namespace Application.ModificationMediatoR.Commands
{
    public class CreateModification : IRequest<Modification>
    {
        public string? Name { get; set; }
        public int ModelId { get; set; }
    }
}
