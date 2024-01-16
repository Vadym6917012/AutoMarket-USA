using Domain.Entities;
using MediatR;

namespace Application.DriveTrainMediatoR.Queries
{
    public class GetAllDriveTrains : IRequest<IEnumerable<DriveTrain>>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
