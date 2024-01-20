using Application.DTOs.Admin;
using MediatR;

namespace Application.Accounts.Queries
{
    public class GetUserById : IRequest<MemberAddEditDto>
    {
        public string Id { get; set; }
    }
}
