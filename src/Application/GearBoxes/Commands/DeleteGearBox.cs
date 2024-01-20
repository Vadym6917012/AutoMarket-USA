using MediatR;

namespace Application.GearBoxes.Commands
{
    public class DeleteGearBox : IRequest
    {
        public int Id { get; set; }
    }
}
