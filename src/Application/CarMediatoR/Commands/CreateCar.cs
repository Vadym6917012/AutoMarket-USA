using Domain.Entities;
using MediatR;

namespace Application.CarMediatoR.Commands
{
    public class CreateCar : IRequest<Car>
    {
        public int Id { get; set; }
        public int ModelId { get; set; }
        public int GenerationId { get; set; }
        public int? ModificationId { get; set; }
        public string? VIN { get; set; }
        public int BodyTypeId { get; set; }
        public int GearBoxTypeId { get; set; }
        public int DriveTrainId { get; set; }
        public int TechnicalConditionId { get; set; }
        public int FuelTypeId { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public int Mileage { get; set; }
        public string? Description { get; set; }
        public string? UserId { get; set; }
        public bool IsAdvertisementApproved { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
