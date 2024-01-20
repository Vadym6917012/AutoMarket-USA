using MediatR;

namespace Application.Admins.Queries
{
    public class GetApplicationRoles : IRequest<IEnumerable<string>>
    {
    }
}
