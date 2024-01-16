using MediatR;

namespace Application.ModelMediatoR.Commands
{
    public class DeleteModel : IRequest
    {
        public int Id { get; set; }
    }
}
