using MediatR;

namespace Application.FuelTypes.Commands
{
    public class UpdateFuelType : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
