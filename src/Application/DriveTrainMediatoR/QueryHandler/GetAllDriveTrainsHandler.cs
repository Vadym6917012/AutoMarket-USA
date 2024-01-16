using Application.Common.Interfaces;
using Application.DriveTrainMediatoR.Queries;
using Domain.Entities;
using MediatR;

namespace Application.DriveTrainMediatoR.QueryHandler
{
    public class GetAllDriveTrainsHandler : IRequestHandler<GetAllDriveTrains, IEnumerable<DriveTrain>>
    {
        private readonly IRepository<DriveTrain> _repository;

        public GetAllDriveTrainsHandler(IRepository<DriveTrain> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<DriveTrain>> Handle(GetAllDriveTrains request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync();
        }
    }
}
