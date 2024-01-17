using Application.AccountMediatoR.Queries;
using Application.Common.Interfaces;
using Application.DTOs.Account;
using MediatR;

namespace Application.AccountMediatoR.QueryHandler
{
    public class RefreshPageQueryHandler : IRequestHandler<RefreshPageQuery, ApplicationUserDTO>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAccountRepository _accountRepository;

        public RefreshPageQueryHandler(IAccountRepository accountRepository, IUserRepository userRepository)
        {
            _accountRepository = accountRepository;
            _userRepository = userRepository;
        }

        public async Task<ApplicationUserDTO> Handle(RefreshPageQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return null;
            }

            return await _accountRepository.CreateApplicationUserDTO(user);
        }
    }
}
