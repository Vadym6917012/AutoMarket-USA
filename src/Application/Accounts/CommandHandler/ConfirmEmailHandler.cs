using Application.Accounts.Commands;
using Application.Common.Interfaces;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Accounts.CommandHandler
{
    public class ConfirmEmailHandler : IRequestHandler<ConfirmEmailCommand, bool>
    {
        private readonly IUserRepository _userRepository;

        public ConfirmEmailHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Email адреса ще не зареєстрована");
            }

            if (user.EmailConfirmed == true)
            {
                throw new ValidationException("Ваш email вже підтверджений, увійдіть в особистий кабінет");
            }

            try
            {
                if (await _userRepository.ConfirmEmailAsync(request.Email, request.Token))
                {
                    return true;
                }

                throw new ValidationException("Неправильний токен, спробуйте пізніше");
            }
            catch (Exception ex)
            {
                throw new($"Помилка підтвердження email, спробуйте пізніше: {ex.Message}");
            }
        }
    }
}
