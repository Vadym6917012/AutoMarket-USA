using MediatR;

namespace Application.TechnicalConditions.Queries
{
    public class GetAllTechnicalConditions : IRequest<IEnumerable<TechnicalCondition>>
    {
    }
}
