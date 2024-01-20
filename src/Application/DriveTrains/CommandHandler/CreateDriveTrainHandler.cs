using Application.Common.Interfaces;
using Application.DriveTrains.Commands;
using MediatR;

namespace Application.DriveTrains.CommandHandler
{
    public class CreateDriveTrainHandler : IRequestHandler<CreateDriveTrain, DriveTrain>
    {
        private readonly IRepository<DriveTrain> _driveTrainRepository;

        public CreateDriveTrainHandler(IRepository<DriveTrain> driveTrainRepository)
        {
            _driveTrainRepository = driveTrainRepository;
        }

        public async Task<DriveTrain> Handle(CreateDriveTrain request, CancellationToken cancellationToken)
        {
            var driveTrain = new DriveTrain
            {
                Name = request.Name,
            };

            return await _driveTrainRepository.AddAsync(driveTrain);
        }
    }
}
