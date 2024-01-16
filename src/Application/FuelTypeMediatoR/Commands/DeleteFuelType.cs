using MediatR;

namespace Application.FuelTypeMediatoR.Commands
{
    public class DeleteFuelType : IRequest
    {
        public int Id { get; set; }
    }
}
