using Application.AccountMediatoR.Commands;
using Application.Common.Interfaces;
using Ardalis.GuardClauses;
using MediatR;

namespace Application.AccountMediatoR.CommandHandler
{
    public class UpdateUserHandler : IRequestHandler<UpdateUser>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(UpdateUser request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindByIdAsync(request.Id);

            Guard.Against.NotFound(request.Id, user);

            user.FirstName = request.FirstName.ToLower();
            user.LastName = request.LastName.ToLower();
            user.UserName = request.UserName.ToLower();
            user.PhoneNumber = request.PhoneNumber.ToLower();

            if (!string.IsNullOrEmpty(request.Password))
            {
                await _userRepository.RemovePasswordAsync(user);
                await _userRepository.AddPasswordAsync(user, request.Password);
            }

            var userRoles = await _userRepository.GetRolesAsync(user);

            await _userRepository.RemoveFromRolesAsync(user, userRoles);

            foreach (var role in request.Roles.Split(",").ToArray())
            {
                var roleToAdd = await _userRepository.GetRolesAsync(role);
                if (roleToAdd != null)
                {
                    await _userRepository.AddToRoleAsync(user, role);
                }
            }
        }
    }
}
