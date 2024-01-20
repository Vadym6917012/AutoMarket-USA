using MediatR;

namespace Application.BodyTypes.Commands
{
    public class UpdateBodyType : IRequest
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
