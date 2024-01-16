using MediatR;

namespace Application.TechnicalConditionMediatoR.Commands
{
    public class UpdateTechnicalCondition : IRequest
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
