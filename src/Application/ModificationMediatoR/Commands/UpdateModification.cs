using MediatR;

namespace Application.ModificationMediatoR.Commands
{
    public class UpdateModification : IRequest
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int ModelId { get; set; }
    }
}
