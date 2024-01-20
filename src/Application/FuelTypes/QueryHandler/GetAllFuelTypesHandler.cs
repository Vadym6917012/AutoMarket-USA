using Application.Common.Interfaces;
using Application.FuelTypes.Queries;
using MediatR;

namespace Application.FuelTypes.QueryHandler
{
    public class GetAllFuelTypesHandler : IRequestHandler<GetAllFuelTypes, IEnumerable<FuelType>>
    {
        private readonly IRepository<FuelType> _repository;

        public GetAllFuelTypesHandler(IRepository<FuelType> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<FuelType>> Handle(GetAllFuelTypes request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync();
        }
    }
}
