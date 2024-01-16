using MediatR;

namespace Application.DriveTrainMediatoR.Commands
{
    public class UpdateDriveTrain : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
