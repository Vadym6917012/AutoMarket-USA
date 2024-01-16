using MediatR;

namespace Application.ModificationMediatoR.Commands
{
    public class DeleteModification : IRequest
    {
        public int Id { get; set; }
    }
}
