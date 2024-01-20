using MediatR;

namespace Application.DriveTrains.Commands
{
    public class CreateDriveTrain : IRequest<DriveTrain>
    {
        public string? Name { get; set; }
    }
}
