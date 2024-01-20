using MediatR;

namespace Application.GearBoxes.Commands
{
    public class CreateGearBox : IRequest<GearBoxType>
    {
        public string Name { get; set; }
    }
}
