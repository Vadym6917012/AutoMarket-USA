using MediatR;

namespace Application.Accounts.Commands
{
    public class ConfirmEmailCommand : IRequest<bool>
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
