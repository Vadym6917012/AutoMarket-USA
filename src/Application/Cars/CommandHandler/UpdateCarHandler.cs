using Application.Cars.Commands;
using Application.Common.Interfaces;
using Ardalis.GuardClauses;
using MediatR;

namespace Application.Cars.CommandHandler
{
    public class UpdateCarHandler : IRequestHandler<UpdateCar, Car>
    {
        private readonly ICarRepository _carRepository;

        public UpdateCarHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<Car> Handle(UpdateCar request, CancellationToken cancellationToken)
        {
            var entity = await _carRepository.GetByIdAsync(request.Id);

            Guard.Against.NotFound(request.Id, entity);

            entity.ModelId = request.ModelId;
            entity.GenerationId = request.GenerationId;
            entity.ModificationId = request.ModificationId;
            entity.VIN = request.VIN;
            entity.BodyTypeId = request.BodyTypeId;
            entity.GearBoxTypeId = request.GearBoxTypeId;
            entity.DriveTrainId = request.DriveTrainId;
            entity.FuelTypeId = request.FuelTypeId;
            entity.Year = request.Year;
            entity.Price = request.Price;
            entity.Mileage = request.Mileage;
            entity.Description = request.Description;
            entity.TechnicalConditionId = request.TechnicalConditionId;
            entity.UserId = request.UserId;
            entity.IsAdvertisementApproved = request.IsAdvertisementApproved;

            return await _carRepository.UpdateAsync(entity);
        }
    }
}
