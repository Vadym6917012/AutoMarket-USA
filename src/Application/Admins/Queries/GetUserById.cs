using Application.DTOs.Admin;
using MediatR;

namespace Application.Admins.Queries
{
    public class GetUserById : IRequest<MemberAddEditDto>
    {
        public string Id { get; set; }
    }
}
