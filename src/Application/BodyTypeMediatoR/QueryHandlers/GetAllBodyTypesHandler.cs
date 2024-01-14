using Application.BodyTypeMediatoR.Queries;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.BodyTypeMediatoR.QueryHandlers
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
