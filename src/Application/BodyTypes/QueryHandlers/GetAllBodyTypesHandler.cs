using Application.BodyTypes.Queries;
using Application.Common.Interfaces;
using MediatR;

namespace Application.BodyTypes.QueryHandlers
{
    public class GetAllBodyTypesHandler : IRequestHandler<GetAllBodyTypes, IEnumerable<BodyType>>
    {
        private readonly IRepository<BodyType> _repository;

        public GetAllBodyTypesHandler(IRepository<BodyType> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<BodyType>> Handle(GetAllBodyTypes request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync();
        }
    }
}
