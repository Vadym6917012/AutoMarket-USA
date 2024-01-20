using MediatR;

namespace Application.TechnicalConditions.Commands
{
    public class DeleteTechnicalCondition : IRequest
    {
        public int Id { get; set; }
    }
}
