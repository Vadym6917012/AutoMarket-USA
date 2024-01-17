using Application.AccountMediatoR.Commands;
using Application.Common.Interfaces;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.AccountMediatoR.CommandHandler
{
    public class ResendEmailConfirmationLinkCommandHandler : IRequestHandler<ResendEmailConfirmationLinkCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAccountRepository _accountRepository;

        public ResendEmailConfirmationLinkCommandHandler(IUserRepository userRepository, IAccountRepository accountRepository)
        {
            _userRepository = userRepository;
            _accountRepository = accountRepository;
        }

        public async Task<bool> Handle(ResendEmailConfirmationLinkCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Цей email ще не був зареєстрований");
            }
            if (user.EmailConfirmed == true)
            {
                throw new ValidationException("Ваш email вже підтверджений, увійдіть в особистий кабінет");
            }

            try
            {
                if (await _accountRepository.SendConfirmEmailAsync(user))
                {
                    return true;
                }
                throw new Exception("Не вдалося відправити електронного листа.");
            }
            catch (Exception)
            {
                throw new Exception("Не вдалося відправити електронного листа.");
            }
        }
    }
}
