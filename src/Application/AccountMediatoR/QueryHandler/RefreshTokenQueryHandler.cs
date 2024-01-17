using Application.AccountMediatoR.Queries;
using Application.Common.Interfaces;
using MediatR;

namespace Application.AccountMediatoR.QueryHandler
{
    public class RefreshTokenQueryHandler : IRequestHandler<RefreshTokenQuery, RefreshTokenResult>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserRepository _userRepository;

        public RefreshTokenQueryHandler(IUserRepository userRepository, IAccountRepository accountRepository)
        {
            _userRepository = userRepository;
            _accountRepository = accountRepository;
        }

        public async Task<RefreshTokenResult> Handle(RefreshTokenQuery request, CancellationToken cancellationToken)
        {
            if (_accountRepository.IsValidRefreshTokenAsync(request.UserId, request.Token).GetAwaiter().GetResult())
            {
                var user = await _userRepository.FindByIdAsync(request.UserId);

                if (user == null) throw new UnauthorizedAccessException("Недійсний або застарілий токен. Будь ласка, спробуйте увійти знову.");

                var refreshToken = await _accountRepository.SaveRefreshTokenAsync(user);

                var result = await _accountRepository.RefreshToken(request.UserId, refreshToken.Token);

                return new RefreshTokenResult
                {
                    ApplicationUserDTO = result,
                    RefreshedToken = refreshToken.Token,
                    DateExpiresUtc = refreshToken.DateExpiresUtc
                };
            }
            else
            {
                return null;
            }
        }
    }
}
