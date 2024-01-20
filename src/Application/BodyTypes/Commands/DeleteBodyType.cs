using MediatR;

namespace Application.BodyTypes.Commands
{
    public class DeleteBodyType : IRequest
    {
        public int Id { get; set; }
    }
}
