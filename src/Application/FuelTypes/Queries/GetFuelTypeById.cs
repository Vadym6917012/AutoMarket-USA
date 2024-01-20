using MediatR;

namespace Application.FuelTypes.Queries
{
    public class GetFuelTypeById : IRequest<FuelType>
    {
        public int Id { get; set; }
    }
}
