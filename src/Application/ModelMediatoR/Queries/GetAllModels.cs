using Domain.Entities;
using MediatR;

namespace Application.ModelMediatoR.Queries
{
    public class GetAllModels : IRequest<IEnumerable<Model>>
    {
    }
}
