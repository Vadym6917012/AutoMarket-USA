using MediatR;

namespace Application.GearBoxMediatoR.Commands
{
    public class DeleteGearBox : IRequest
    {
        public int Id { get; set; }
    }
}
