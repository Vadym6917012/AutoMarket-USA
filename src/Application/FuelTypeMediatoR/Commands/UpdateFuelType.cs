using MediatR;

namespace Application.FuelTypeMediatoR.Commands
{
    public class UpdateFuelType : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
