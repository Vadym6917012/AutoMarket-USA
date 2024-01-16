using Application.Common.Interfaces;
using Application.DriveTrainMediatoR.Queries;
using Domain.Entities;
using MediatR;

namespace Application.DriveTrainMediatoR.QueryHandler
{
    public class GetDriveTrainByIdHandler : IRequestHandler<GetDriveTrainById, DriveTrain>
    {
        private readonly IRepository<DriveTrain> _driveTrainRepository;

        public GetDriveTrainByIdHandler(IRepository<DriveTrain> driveTrainRepository)
        {
            _driveTrainRepository = driveTrainRepository;
        }

        public async Task<DriveTrain> Handle(GetDriveTrainById request, CancellationToken cancellationToken)
        {
            return await _driveTrainRepository.GetByIdAsync(request.Id);
        }
    }
}
