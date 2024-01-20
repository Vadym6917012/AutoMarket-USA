using MediatR;

namespace Application.Models.Commands
{
    public class DeleteModel : IRequest
    {
        public int Id { get; set; }
    }
}
