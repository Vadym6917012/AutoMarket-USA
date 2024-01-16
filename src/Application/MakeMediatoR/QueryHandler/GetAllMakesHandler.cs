using Application.Common.Interfaces;
using Application.MakeMediatoR.Queries;
using Domain.Entities;
using MediatR;

namespace Application.MakeMediatoR.QueryHandler
{
    public class GetAllMakesHandler : IRequestHandler<GetAllMakes, IEnumerable<Make>>
    {
        private readonly IMakeRepository _repository;

        public GetAllMakesHandler(IMakeRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Make>> Handle(GetAllMakes request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync();
        }
    }
}
