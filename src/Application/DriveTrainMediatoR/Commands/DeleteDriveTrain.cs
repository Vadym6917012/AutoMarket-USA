using MediatR;

namespace Application.DriveTrainMediatoR.Commands
{
    public class DeleteDriveTrain : IRequest
    {
        public int Id { get; set; }
    }
}
