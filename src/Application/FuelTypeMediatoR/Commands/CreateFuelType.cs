using Domain.Entities;
using MediatR;

namespace Application.FuelTypeMediatoR.Commands
{
    public class CreateFuelType : IRequest<FuelType>
    {
        public string Name { get; set; }
    }
}
