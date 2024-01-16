using Application.Common.Interfaces;
using Application.MakeMediatoR.Commands;
using Domain.Entities;
using MediatR;

namespace Application.MakeMediatoR.CommandHandler
{
    public class CreateMakeHandler : IRequestHandler<CreateMake, Make>
    {
        private readonly IMakeRepository _repository;

        public CreateMakeHandler(IMakeRepository repository)
        {
            _repository = repository;
        }

        public async Task<Make> Handle(CreateMake request, CancellationToken cancellationToken)
        {
            var entity = new Make
            {
                Name = request.Name,
                ImagePath = request.ImagePath,
                ProducingCountryId = request.ProducingCountryId,
            };

            return await _repository.AddAsync(entity);
        }
    }
}
