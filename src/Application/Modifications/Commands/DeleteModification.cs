using MediatR;

namespace Application.Modifications.Commands
{
    public class DeleteModification : IRequest
    {
        public int Id { get; set; }
    }
}
