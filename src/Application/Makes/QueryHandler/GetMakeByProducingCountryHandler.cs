using Application.Common.Interfaces;
using Application.Makes.Queries;
using MediatR;

namespace Application.Makes.QueryHandler
{
    public class GetMakeByProducingCountryHandler : IRequestHandler<GetMakeByProducingCountry, IEnumerable<Make>>
    {
        private readonly IMakeRepository _repository;

        public GetMakeByProducingCountryHandler(IMakeRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Make>> Handle(GetMakeByProducingCountry request, CancellationToken cancellationToken)
        {
            return _repository.GetMakeByCountry(request.Id);
        }
    }
}
