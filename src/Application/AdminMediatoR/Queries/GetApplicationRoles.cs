using MediatR;

namespace Application.AdminMediatoR.Queries
{
    public class GetApplicationRoles : IRequest<IEnumerable<string>>
    {
    }
}
