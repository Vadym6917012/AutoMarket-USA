using Domain.Entities;
using MediatR;

namespace Application.GearBoxMediatoR.Queries
{
    public class GetGearBoxById : IRequest<GearBoxType>
    {
        public int Id { get; set; }
    }
}
