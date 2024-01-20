using Application.Common.Interfaces;
using Application.GearBoxes.Commands;
using MediatR;

namespace Application.GearBoxes.CommandHandler
{
    public class CreateGearBoxHandler : IRequestHandler<CreateGearBox, GearBoxType>
    {
        private readonly IRepository<GearBoxType> _repository;

        public CreateGearBoxHandler(IRepository<GearBoxType> repository)
        {
            _repository = repository;
        }

        public async Task<GearBoxType> Handle(CreateGearBox request, CancellationToken cancellationToken)
        {
            var entity = new GearBoxType
            {
                Name = request.Name,
            };

            return await _repository.AddAsync(entity);
        }
    }
}
