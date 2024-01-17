using MediatR;

namespace Application.AccountMediatoR.Commands
{
    public class ConfirmEmailCommand : IRequest<bool>
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
