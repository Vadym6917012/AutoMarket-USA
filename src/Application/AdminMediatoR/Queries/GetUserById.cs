using Application.DTOs.Admin;
using MediatR;

namespace Application.AdminMediatoR.Queries
{
    public class GetUserById : IRequest<MemberAddEditDto>
    {
        public string Id { get; set; }
    }
}
