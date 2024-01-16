using Domain.Entities;
using MediatR;

namespace Application.TechnicalConditionMediatoR.Commands
{
    public class CreateTechnicalCondition : IRequest<TechnicalCondition>
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
