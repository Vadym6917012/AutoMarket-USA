using MediatR;

namespace Application.AccountMediatoR.Commands
{
    public class ForgotUsernameOrPasswordCommand : IRequest<bool>
    {
        public string Email { get; set; }
    }
}
