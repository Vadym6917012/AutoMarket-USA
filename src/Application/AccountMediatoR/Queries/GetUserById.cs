using Application.DTOs.Admin;
using MediatR;

namespace Application.AccountMediatoR.Queries
{
    public class GetUserById : IRequest<MemberAddEditDto>
    {
        public string Id { get; set; }
    }
}
