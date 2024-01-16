using Domain.Entities;
using MediatR;

namespace Application.TechnicalConditionMediatoR.Queries
{
    public class GetTechnicalConditionById : IRequest<TechnicalCondition>
    {
        public int Id { get; set; }
    }
}
