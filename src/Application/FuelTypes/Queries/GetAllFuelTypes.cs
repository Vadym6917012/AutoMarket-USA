using MediatR;

namespace Application.FuelTypes.Queries
{
    public class GetAllFuelTypes : IRequest<IEnumerable<FuelType>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
