using MediatR;

namespace Application.AccountMediatoR.Commands
{
    public class ResetPasswordCommand : IRequest<bool>
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string NewPassword { get; set; }
        public string DecodedToken { get; set; }
    }
}
