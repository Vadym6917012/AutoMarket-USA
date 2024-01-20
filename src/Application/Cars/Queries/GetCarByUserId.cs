using MediatR;

namespace Application.Cars.Queries
{
    public class GetCarByUserId : IRequest<IEnumerable<Car>>
    {
        public string UserId { get; set; }
    }
}
