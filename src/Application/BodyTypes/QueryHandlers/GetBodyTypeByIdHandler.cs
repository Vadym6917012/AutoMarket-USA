using Application.BodyTypes.Queries;
using Application.Common.Interfaces;
using MediatR;

namespace Application.BodyTypes.QueryHandlers
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
