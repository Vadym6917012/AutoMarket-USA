using Application.DTOs.Account;
using MediatR;

namespace Application.AccountMediatoR.Commands
{
    public class RegisterUserCommand : IRequest
    {
        public RegisterDTO RegistrationData { get; set; }
    }
}
