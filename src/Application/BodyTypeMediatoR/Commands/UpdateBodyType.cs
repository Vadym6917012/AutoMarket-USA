using MediatR;

namespace Application.BodyTypeMediatoR.Commands
{
    public class UpdateBodyType : IRequest
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
