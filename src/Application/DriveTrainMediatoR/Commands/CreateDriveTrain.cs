using Domain.Entities;
using MediatR;

namespace Application.DriveTrainMediatoR.Commands
{
    public class CreateDriveTrain : IRequest<DriveTrain>
    {
        public string? Name { get; set; }
    }
}
