using MediatR;

namespace Application.Modifications.Commands
{
    public class CreateModification : IRequest<Modification>
    {
        public string? Name { get; set; }
        public int ModelId { get; set; }
    }
}
