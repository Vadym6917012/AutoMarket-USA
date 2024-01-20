using MediatR;

namespace Application.BodyTypes.Commands
{
    public class CreateBodyType : IRequest<BodyType>
    {
        public string? Name { get; set; }
    }
}
