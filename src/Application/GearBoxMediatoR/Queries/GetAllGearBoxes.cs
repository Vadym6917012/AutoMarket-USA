using Domain.Entities;
using MediatR;

namespace Application.GearBoxMediatoR.Queries
{
    public class GetAllGearBoxes : IRequest<IEnumerable<GearBoxType>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
