using Application.Common.Interfaces;
using Application.ModelMediatoR.Commands;
using Domain.Entities;
using MediatR;

namespace Application.ModelMediatoR.CommandHandler
{
    public class CreateModelHandler : IRequestHandler<CreateModel, Model>
    {
        private readonly IModelRepository _repository;

        public CreateModelHandler(IModelRepository repository)
        {
            _repository = repository;
        }

        public async Task<Model> Handle(CreateModel request, CancellationToken cancellationToken)
        {
            var entity = new Model
            {
                Name = request.Name,
                MakeId = request.MakeId,
            };

            return await _repository.AddAsync(entity);
        }
    }
}
