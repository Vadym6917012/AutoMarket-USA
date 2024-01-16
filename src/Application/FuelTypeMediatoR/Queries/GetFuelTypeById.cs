using Domain.Entities;
using MediatR;

namespace Application.FuelTypeMediatoR.Queries
{
    public class GetFuelTypeById : IRequest<FuelType>
    {
        public int Id { get; set; }
    }
}
