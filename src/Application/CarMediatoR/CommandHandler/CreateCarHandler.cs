using Application.CarMediatoR.Commands;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.CarMediatoR.CommandHandler
{
    public class CreateCarHandler : IRequestHandler<CreateCar, Car>
    {
        private readonly ICarRepository _carRepository;

        public CreateCarHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<Car> Handle(CreateCar request, CancellationToken cancellationToken)
        {
            var car = new Car
            {
                ModelId = request.ModelId,
                GenerationId = request.GenerationId,
                ModificationId = request.ModificationId,
                VIN = request.VIN,
                BodyTypeId = request.BodyTypeId,
                GearBoxTypeId = request.GearBoxTypeId,
                DriveTrainId = request.DriveTrainId,
                FuelTypeId = request.FuelTypeId,
                Year = request.Year,
                Price = request.Price,
                Mileage = request.Mileage,
                Description = request.Description,
                TechnicalConditionId = request.TechnicalConditionId,
                UserId = request.UserId,
                IsAdvertisementApproved = request.IsAdvertisementApproved,
                DateCreated = request.DateCreated,
            };

            return await _carRepository.AddAsync(car);
        }
    }
}
