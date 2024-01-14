using Application.BodyTypeMediatoR.Queries;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.BodyTypeMediatoR.QueryHandlers
{
    public class GetBodyTypeByIdHandler : IRequestHandler<GetBodyTypeById, BodyType>
    {
        private readonly IRepository<BodyType> _bodyTypeRepository;

        public GetBodyTypeByIdHandler(IRepository<BodyType> bodyTypeRepository)
        {
            _bodyTypeRepository = bodyTypeRepository;
        }

        public async Task<BodyType> Handle(GetBodyTypeById request, CancellationToken cancellationToken)
        {
            return await _bodyTypeRepository.GetByIdAsync(request.Id);
        }
    }
}
