using MediatR;

namespace Application.FuelTypes.Commands
{
    public class CreateFuelType : IRequest<FuelType>
    {
        public string Name { get; set; }
    }
}
