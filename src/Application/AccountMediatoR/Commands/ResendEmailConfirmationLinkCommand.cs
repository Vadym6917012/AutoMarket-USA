using MediatR;

namespace Application.AccountMediatoR.Commands
{
    public class ResendEmailConfirmationLinkCommand : IRequest<bool>
    {
        public string Email { get; set; }
    }
}
