using MediatR;

namespace Application.Accounts.Commands
{
    public class ForgotUsernameOrPasswordCommand : IRequest<bool>
    {
        public string Email { get; set; }
    }
}
