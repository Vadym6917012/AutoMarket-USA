using MediatR;

namespace Application.BodyTypeMediatoR.Commands
{
    public class DeleteBodyType : IRequest
    {
        public int Id { get; set; }
    }
}
