using Application.AdminMediatoR.Queries;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.AdminMediatoR.QueryHandler
{
    public class GetApplicationRolesHandler : IRequestHandler<GetApplicationRoles, IEnumerable<string>>
    {
        private readonly IAdminRepository<ApplicationUser> _repository;

        public GetApplicationRolesHandler(Common.Interfaces.IAdminRepository<ApplicationUser> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<string>> Handle(GetApplicationRoles request, CancellationToken cancellationToken)
        {
            return await _repository.GetApplicationRolesAsync();
        }
    }
}
