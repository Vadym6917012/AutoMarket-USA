using Application.Common.Interfaces;
using Application.FuelTypeMediatoR.Commands;
using Domain.Entities;
using MediatR;

namespace Application.FuelTypeMediatoR.CommandHandler
{
    public class CreateFuelTypeHandler : IRequestHandler<CreateFuelType, FuelType>
    {
        private readonly IRepository<FuelType> _repository;

        public CreateFuelTypeHandler(IRepository<FuelType> repository)
        {
            _repository = repository;
        }

        public async Task<FuelType> Handle(CreateFuelType request, CancellationToken cancellationToken)
        {
            var fuelType = new FuelType
            {
                Name = request.Name,
            };

            return await _repository.AddAsync(fuelType);
        }
    }
}
