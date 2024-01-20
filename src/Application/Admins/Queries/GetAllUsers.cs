using Application.DTOs.Admin;
using MediatR;

namespace Application.Admins.Queries
{
    public class GetAllUsers : IRequest<IEnumerable<MemberViewDto>>
    {
    }
}
