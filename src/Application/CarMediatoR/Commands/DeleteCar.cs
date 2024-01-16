using MediatR;

namespace Application.CarMediatoR.Commands
{
    public class DeleteCar : IRequest
    {
        public int Id { get; set; }
    }
}
