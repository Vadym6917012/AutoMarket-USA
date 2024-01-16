using Application.Common.Interfaces;
using Application.GearBoxMediatoR.Queries;
using Ardalis.GuardClauses;
using Domain.Entities;
using MediatR;

namespace Application.GearBoxMediatoR.QueryHandler
{
    public class GetGearBoxByIdHandler : IRequestHandler<GetGearBoxById, GearBoxType>
    {
        private readonly IRepository<GearBoxType> _repository;

        public GetGearBoxByIdHandler(IRepository<GearBoxType> repository)
        {
            _repository = repository;
        }

        public async Task<GearBoxType> Handle(GetGearBoxById request, CancellationToken cancellationToken)
        {
            var entiry = await _repository.GetByIdAsync(request.Id);

            Guard.Against.NotFound(request.Id, entiry);

            return entiry;
        }
    }
}
