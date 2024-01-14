using Application.BodyTypeMediatoR.Commands;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.BodyTypeMediatoR.CommandHandlers
{
    public class CreateBodyTypeHandler : IRequestHandler<CreateBodyType, BodyType>
    {
        private readonly IRepository<BodyType> _bodyTypeRepository;

        public CreateBodyTypeHandler(IRepository<BodyType> bodyTypeRepository)
        {
            _bodyTypeRepository = bodyTypeRepository;
        }

        public async Task<BodyType> Handle(CreateBodyType request, CancellationToken cancellationToken)
        {
            var bodyType = new BodyType
            {
                Name = request.Name
            };

            return await _bodyTypeRepository.AddAsync(bodyType);
        }
    }
}
