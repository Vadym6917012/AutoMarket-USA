using Domain.Entities;
using MediatR;

namespace Application.TechnicalConditionMediatoR.Queries
{
    public class GetAllTechnicalConditions : IRequest<IEnumerable<TechnicalCondition>>
    {
    }
}
