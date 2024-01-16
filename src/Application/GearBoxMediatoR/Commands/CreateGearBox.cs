using Domain.Entities;
using MediatR;

namespace Application.GearBoxMediatoR.Commands
{
    public class CreateGearBox : IRequest<GearBoxType>
    {
        public string Name { get; set; }
    }
}
