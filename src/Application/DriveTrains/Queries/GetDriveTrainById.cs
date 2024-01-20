using MediatR;

namespace Application.DriveTrains.Queries
{
    public class GetDriveTrainById : IRequest<DriveTrain>
    {
        public int Id { get; set; }
    }
}
