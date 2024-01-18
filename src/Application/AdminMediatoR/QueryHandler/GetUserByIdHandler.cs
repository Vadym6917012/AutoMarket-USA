using Application.AdminMediatoR.Queries;
using Application.Common.Interfaces;
using Application.DTOs.Admin;
using Domain.Entities;
using MediatR;

namespace Application.AdminMediatoR.QueryHandler
{
    public class GetUserByIdHandler : IRequestHandler<GetUserById, MemberAddEditDto>
    {
        private readonly IAdminRepository<ApplicationUser> _repository;

        public GetUserByIdHandler(IAdminRepository<ApplicationUser> repository)
        {
            _repository = repository;
        }

        public async Task<MemberAddEditDto> Handle(GetUserById request, CancellationToken cancellationToken)
        {
            return await _repository.GetMemberAsync(request.Id);
        }
    }
}
