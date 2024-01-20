using MediatR;

namespace Application.Cars.Queries
{
    public class GetCarById : IRequest<Car>
    {
        public int Id { get; set; }
    }
}
