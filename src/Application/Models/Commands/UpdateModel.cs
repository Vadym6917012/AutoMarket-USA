using MediatR;

namespace Application.Models.Commands
{
    public class UpdateModel : IRequest<Model>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MakeId { get; set; }
    }
}
