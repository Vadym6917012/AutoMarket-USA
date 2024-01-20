using MediatR;

namespace Application.Accounts.Commands
{
    public class ResendEmailConfirmationLinkCommand : IRequest<bool>
    {
        public string Email { get; set; }
    }
}
