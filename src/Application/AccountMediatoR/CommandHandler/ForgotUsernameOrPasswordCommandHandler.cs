using Application.AccountMediatoR.Commands;
using Application.Common.Interfaces;
using FluentValidation;
using MediatR;

namespace Application.AccountMediatoR.CommandHandler
{
    public class ForgotUsernameOrPasswordCommandHandler : IRequestHandler<ForgotUsernameOrPasswordCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAccountRepository _accountRepository;

        public ForgotUsernameOrPasswordCommandHandler(IAccountRepository accountRepository, IUserRepository userRepository)
        {
            _accountRepository = accountRepository;
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(ForgotUsernameOrPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindByEmailAsync(request.Email);

            if (user == null)
            {
                throw new UnauthorizedAccessException("Такий email ще не зареєстрований");
            }

            if (!user.EmailConfirmed)
            {
                throw new ValidationException("Підтвердіть спочатку ваш email.");
            }

            try
            {
                if (await _accountRepository.SendForgotUsernameOrPassword(user))
                {
                    return true;
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
