using MediatR;

namespace Application.DriveTrains.Queries
{
    public class GetAllDriveTrains : IRequest<IEnumerable<DriveTrain>>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
