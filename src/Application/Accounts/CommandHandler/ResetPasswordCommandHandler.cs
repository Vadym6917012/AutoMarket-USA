using Application.Accounts.Commands;
using Application.Common.Interfaces;
using Domain.Exceptions;
using MediatR;

namespace Application.Accounts.CommandHandler
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, bool>
    {
        private readonly IUserRepository _userRepository;

        public ResetPasswordCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindByEmailAsync(request.Email);

            if (user == null)
            {
                throw new UnauthorizedAccessException("Такий email ще не зареєстрований");
            }

            if (!user.EmailConfirmed)
            {
                throw new BadRequestException("Підтвердіть спочатку ваш email.");
            }

            try
            {
                var result = await _userRepository.ResetPasswordAsync(user, request.DecodedToken, request.NewPassword);

                if (result.Succeeded)
                {
                    return true;
                }

                throw new BadRequestException("Неправильний токен, спробуйте пізніше");
            }
            catch (Exception)
            {
                throw new BadRequestException("Неправильний токен, спробуйте пізніше");
            }
        }
    }
}
