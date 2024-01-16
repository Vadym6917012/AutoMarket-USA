using MediatR;

namespace Application.GearBoxMediatoR.Commands
{
    public class UpdateGearBox : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
