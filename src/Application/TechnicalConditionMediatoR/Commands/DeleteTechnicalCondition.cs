using MediatR;

namespace Application.TechnicalConditionMediatoR.Commands
{
    public class DeleteTechnicalCondition : IRequest
    {
        public int Id { get; set; }
    }
}
