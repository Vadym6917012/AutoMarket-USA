using Application.AccountMediatoR.Queries;
using Application.Common.Interfaces;
using Application.DTOs.Account;
using Domain.Constants;
using MediatR;

namespace Application.AccountMediatoR.QueryHandler
{
    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, ApplicationUserDTO>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserRepository _userRepository;

        public LoginUserQueryHandler(IUserRepository userRepository, IAccountRepository accountRepository)
        {
            _userRepository = userRepository;
            _accountRepository = accountRepository;
        }

        public async Task<ApplicationUserDTO> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindByEmailAsync(request.UserName);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Неправильне ім`я або пароль");
            }

            if (user.EmailConfirmed == false)
            {
                throw new UnauthorizedAccessException("Підтвердіть ваш email.");
            }

            var result = await _accountRepository.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded)
            {
                if (!user.UserName.Equals(SD.AdminUserName))
                {
                    await _userRepository.AccessFailedAsync(user);
                }

                if (user.AccessFailedCount >= SD.MaximumLoginAttempts)
                {
                    await _userRepository.SetLockoutEndDateAsync(user, DateTime.UtcNow.AddDays(1));
                    throw new UnauthorizedAccessException(string.Format("Ваш акаунт було заблоковано. Вам потрібно зачекати до {0} щоб залогінитися", user.LockoutEnd));
                }
                throw new UnauthorizedAccessException("Неправильне ім`я або пароль");
            }

            await _userRepository.ResetAccessFailedCountAsync(user);
            await _userRepository.SetLockoutEndDateAsync(user, null);

            return await _accountRepository.CreateApplicationUserDTO(user);
        }
    }
}
