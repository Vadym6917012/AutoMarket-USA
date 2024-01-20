using MediatR;

namespace Application.TechnicalConditions.Queries
{
    public class GetTechnicalConditionById : IRequest<TechnicalCondition>
    {
        public int Id { get; set; }
    }
}
