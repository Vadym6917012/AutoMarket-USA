using Application.Common.Interfaces;
using Application.GearBoxMediatoR.Queries;
using Domain.Entities;
using MediatR;

namespace Application.GearBoxMediatoR.QueryHandler
{
    public class GetAllGearBoxesHandler : IRequestHandler<GetAllGearBoxes, IEnumerable<GearBoxType>>
    {
        private readonly IRepository<GearBoxType> _repository;

        public GetAllGearBoxesHandler(IRepository<GearBoxType> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<GearBoxType>> Handle(GetAllGearBoxes request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync();
        }
    }
}
