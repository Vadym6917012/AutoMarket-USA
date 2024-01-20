using MediatR;

namespace Application.DriveTrains.Commands
{
    public class DeleteDriveTrain : IRequest
    {
        public int Id { get; set; }
    }
}
