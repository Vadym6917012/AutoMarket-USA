using Application.DTOs.Account;
using MediatR;

namespace Application.AccountMediatoR.Queries
{
    public class RefreshPageQuery : IRequest<ApplicationUserDTO>
    {
        public string Email { get; set; }
    }
}
