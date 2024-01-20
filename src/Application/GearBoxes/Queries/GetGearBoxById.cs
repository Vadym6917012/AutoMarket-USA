using MediatR;

namespace Application.GearBoxes.Queries
{
    public class GetGearBoxById : IRequest<GearBoxType>
    {
        public int Id { get; set; }
    }
}
