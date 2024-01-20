using MediatR;

namespace Application.Models.Commands
{
    public class CreateModel : IRequest<Model>
    {
        public string Name { get; set; }
        public int MakeId { get; set; }
    }
}
