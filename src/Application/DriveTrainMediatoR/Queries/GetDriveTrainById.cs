using Domain.Entities;
using MediatR;

namespace Application.DriveTrainMediatoR.Queries
{
    public class GetDriveTrainById : IRequest<DriveTrain>
    {
        public int Id { get; set; }
    }
}
