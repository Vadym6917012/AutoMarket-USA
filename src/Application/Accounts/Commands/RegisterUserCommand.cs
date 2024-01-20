using Application.DTOs.Account;
using MediatR;

namespace Application.Accounts.Commands
{
    public class RegisterUserCommand : IRequest
    {
        public RegisterDTO RegistrationData { get; set; }
    }
}
