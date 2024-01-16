using Application.CarMediatoR.Queries;
using Application.Common.Interfaces;
using Application.DTOs.Car;
using Domain.Entities;
using MediatR;

namespace Application.CarMediatoR.QueryHandler
{
    public class CarAdvanceFilterHandler : IRequestHandler<CarAdvanceFilter, IEnumerable<Car>>
    {
        private readonly ICarRepository _carRepository;

        public CarAdvanceFilterHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<IEnumerable<Car>> Handle(CarAdvanceFilter request, CancellationToken cancellationToken)
        {
            var carFilter = new CarFilter
            {
                MakeId = request.MakeId,
                ModelId = request.ModelId,
                GenerationId = request.GenerationId,
                ModificationId = request.ModificationId,
                BodyTypeId = request.BodyTypeId,
                GearBoxTypeId = request.GearBoxTypeId,
                DriveTrainId = request.DriveTrainId,
                TechnicalConditionId = request.TechnicalConditionId,
                FuelTypeId = request.FuelTypeId,
                YearFrom = request.YearFrom,
                YearTo = request.YearTo,
                MileageFrom = request.MileageFrom,
                MileageTo = request.MileageTo,
                PriceFrom = request.PriceFrom,
                PriceTo = request.PriceTo,
            };

            return await _carRepository.CarFilter(carFilter);
        }
    }
}
