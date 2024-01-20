using Application.Common.Interfaces;
using Application.DriveTrains.Commands;
using Ardalis.GuardClauses;
using MediatR;

namespace Application.DriveTrains.CommandHandler
{
    public class UpdateDriveTrainHandler : IRequestHandler<UpdateDriveTrain>
    {
        private readonly IRepository<DriveTrain> _driveTrainRepository;

        public UpdateDriveTrainHandler(IRepository<DriveTrain> driveTrainRepository)
        {
            _driveTrainRepository = driveTrainRepository;
        }

        public async Task Handle(UpdateDriveTrain request, CancellationToken cancellationToken)
        {
            var entity = await _driveTrainRepository.GetByIdAsync(request.Id);

            Guard.Against.NotFound(request.Id, entity);

            entity.Name = request.Name;

            await _driveTrainRepository.UpdateAsync(entity);
        }
    }
}
