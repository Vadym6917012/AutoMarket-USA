using Application.Common.Interfaces;
using Application.GearBoxes.Queries;
using MediatR;

namespace Application.GearBoxes.QueryHandler
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
