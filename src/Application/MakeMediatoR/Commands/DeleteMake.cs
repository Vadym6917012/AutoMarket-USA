using MediatR;

namespace Application.MakeMediatoR.Commands
{
    public class DeleteMake : IRequest
    {
        public int Id { get; set; }
    }
}
