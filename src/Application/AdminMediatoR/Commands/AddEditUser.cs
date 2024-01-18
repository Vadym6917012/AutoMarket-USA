using Application.DTOs.Admin;
using MediatR;

namespace Application.AdminMediatoR.Commands
{
    public class AddEditUser : IRequest<AddEditUser>
    {
        public MemberAddEditDto model { get; set; }
    }
}
