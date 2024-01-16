﻿using Application.Common.Interfaces;
using Application.DriveTrainMediatoR.Commands;
using Ardalis.GuardClauses;
using Domain.Entities;
using MediatR;

namespace Application.DriveTrainMediatoR.CommandHandler
{
    public class DeleteDriveTrainHandler : IRequestHandler<DeleteDriveTrain>
    {
        private readonly IRepository<DriveTrain> _driveTrainRepository;

        public DeleteDriveTrainHandler(IRepository<DriveTrain> driveTrainRepository)
        {
            _driveTrainRepository = driveTrainRepository;
        }

        public async Task Handle(DeleteDriveTrain request, CancellationToken cancellationToken)
        {
            var entity = await _driveTrainRepository.GetByIdAsync(request.Id);

            Guard.Against.NotFound(request.Id, entity);

            await _driveTrainRepository.DeleteAsync(entity);
        }
    }
}
