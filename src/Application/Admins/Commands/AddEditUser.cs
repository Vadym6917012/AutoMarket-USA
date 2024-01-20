using Application.DTOs.Admin;
using MediatR;

namespace Application.Admins.Commands
{
    public class AddEditUser : IRequest<AddEditUser>
    {
        public MemberAddEditDto model { get; set; }
    }
}
