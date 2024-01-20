using Application.Admins.Commands;
using Application.Common.Interfaces;
using Domain.Constants;
using Domain.Exceptions;
using FluentValidation;
using MediatR;

namespace Application.Admins.CommandHandler
{
    public class AddEditUserHandler : IRequestHandler<AddEditUser, AddEditUser>
    {
        private readonly IAdminRepository<ApplicationUser> _repository;

        public AddEditUserHandler(IAdminRepository<ApplicationUser> repository)
        {
            _repository = repository;
        }

        public async Task<AddEditUser> Handle(AddEditUser request, CancellationToken cancellationToken)
        {
            ApplicationUser user;

            if (string.IsNullOrEmpty(request.model.Id))
            {
                if (string.IsNullOrEmpty(request.model.Password) || request.model.Password.Length < 6)
                {
                    throw new ValidationException("Пароль повинен складатися зі шести або більше символів.");
                }

                user = await _repository.CreateAsync(request.model, request.model.Password);

                if (user == null)
                {
                    throw new BadRequestException("Не вдалося створити користувача");
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(request.model.Password))
                {
                    if (request.model.Password.Length < 6)
                    {
                        throw new ValidationException("Пароль повинен складатися зі шести або більше символів.");
                    }
                }

                if (_repository.IsAdminUserId(request.model.Id))
                {
                    throw new BadRequestException(SD.SuperAdminChangeNotAllowed);
                }

                user = await _repository.FindByIdAsync(request.model.Id);
                if (user == null)
                {
                    throw new NotFound("Юзера за таким Id не знайдено");
                }

                await _repository.UpdateAsync(request.model.Id, request.model, request.model.Password);
            }

            var userRoles = await _repository.GetRolesAsync(user);

            await _repository.RemoveFromRolesAsync(user, userRoles);

            foreach (var role in request.model.Roles.Split(",").ToArray())
            {
                var roleToAdd = await _repository.GetRolesAsync(role);
                if (roleToAdd != null)
                {
                    await _repository.AddToRoleAsync(user, role);
                }
            }

            return request;
        }
    }
}
