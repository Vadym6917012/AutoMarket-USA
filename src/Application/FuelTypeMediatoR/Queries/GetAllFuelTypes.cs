using Domain.Entities;
using MediatR;

namespace Application.FuelTypeMediatoR.Queries
{
    public class GetAllFuelTypes : IRequest<IEnumerable<FuelType>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
