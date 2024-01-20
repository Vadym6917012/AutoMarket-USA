using Application.Accounts.Commands;
using Application.Common.Interfaces;
using Domain.Constants;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Accounts.CommandHandler
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserRepository _userRepository;

        public RegisterUserCommandHandler(IAccountRepository accountRepository, IUserRepository userRepository)
        {
            _accountRepository = accountRepository;
            _userRepository = userRepository;
        }

        public async Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var model = request.RegistrationData;

            if (await _userRepository.CheckEmailExistAsync(model.Email))
            {
                throw new ValidationException($"Існуючий акаунт вже є під таким email: {model.Email}");
            }

            var user = await _userRepository.CreateUserAsync(model, SD.MemberRole);

            try
            {
                if (await _accountRepository.SendConfirmEmailAsync(user))
                {
                    return;
                }

                throw new Exception("Не вдалося відправити електронного листа. Зверніться до адміністратора для отримання додаткової допомоги.");
            }
            catch (Exception)
            {
                throw new Exception("Не вдалося відправити електронного листа. Зверніться до адміністратора для отримання додаткової допомоги.");
            }
        }
    }
}
