using MediatR;

namespace Application.Makes.Commands
{
    public class DeleteMake : IRequest
    {
        public int Id { get; set; }
    }
}
