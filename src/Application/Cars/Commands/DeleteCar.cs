using MediatR;

namespace Application.Cars.Commands
{
    public class DeleteCar : IRequest
    {
        public int Id { get; set; }
    }
}
