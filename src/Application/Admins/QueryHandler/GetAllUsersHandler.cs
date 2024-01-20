using Application.Admins.Queries;
using Application.Common.Interfaces;
using Application.DTOs.Admin;
using MediatR;

namespace Application.Admins.QueryHandler
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUsers, IEnumerable<MemberViewDto>>
    {
        private readonly IAdminRepository<ApplicationUser> _adminRepository;

        public GetAllUsersHandler(IAdminRepository<ApplicationUser> adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public async Task<IEnumerable<MemberViewDto>> Handle(GetAllUsers request, CancellationToken cancellationToken)
        {
            return await _adminRepository.GetMembersAsync();
        }
    }
}
