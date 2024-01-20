using Application.Admins.Queries;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Admins.QueryHandler
{
    public class GetApplicationRolesHandler : IRequestHandler<GetApplicationRoles, IEnumerable<string>>
    {
        private readonly IAdminRepository<ApplicationUser> _repository;

        public GetApplicationRolesHandler(IAdminRepository<ApplicationUser> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<string>> Handle(GetApplicationRoles request, CancellationToken cancellationToken)
        {
            return await _repository.GetApplicationRolesAsync();
        }
    }
}
