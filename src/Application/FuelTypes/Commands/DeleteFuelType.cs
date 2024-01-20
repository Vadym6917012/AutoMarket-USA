using MediatR;

namespace Application.FuelTypes.Commands
{
    public class DeleteFuelType : IRequest
    {
        public int Id { get; set; }
    }
}
