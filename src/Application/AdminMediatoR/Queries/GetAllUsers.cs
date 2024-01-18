using Application.DTOs.Admin;
using MediatR;

namespace Application.AdminMediatoR.Queries
{
    public class GetAllUsers : IRequest<IEnumerable<MemberViewDto>>
    {
    }
}
