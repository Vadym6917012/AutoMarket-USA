using MediatR;

namespace Application.TechnicalConditions.Commands
{
    public class CreateTechnicalCondition : IRequest<TechnicalCondition>
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
