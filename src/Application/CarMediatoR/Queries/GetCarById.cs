using Domain.Entities;
using MediatR;

namespace Application.CarMediatoR.Queries
{
    public class GetCarById : IRequest<Car>
    {
        public int Id { get; set; }
    }
}
