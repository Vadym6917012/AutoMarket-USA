using MediatR;

namespace Application.GearBoxes.Queries
{
    public class GetAllGearBoxes : IRequest<IEnumerable<GearBoxType>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
