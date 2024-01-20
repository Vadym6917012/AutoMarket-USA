using Application.DTOs.Account;
using MediatR;

namespace Application.Accounts.Queries
{
    public class LoginUserQuery : IRequest<ApplicationUserDTO>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
