using MediatR;

namespace Application.Models.Queries
{
    public class GetAllModels : IRequest<IEnumerable<Model>>
    {
    }
}
