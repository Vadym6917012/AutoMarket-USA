using Application.Admins.Commands;
using Application.Common.Interfaces;
using Domain.Constants;
using Domain.Exceptions;
using MediatR;

namespace Application.Admins.CommandHandler
{
    public class DeleteUserHandler : IRequestHandler<DeleteUser>
    {
        private readonly IAdminRepository<ApplicationUser> _repository;

        public DeleteUserHandler(IAdminRepository<ApplicationUser> repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteUser request, CancellationToken cancellationToken)
        {
            var user = await _repository.FindByIdAsync(request.Id);
            if (user == null)
            {
                throw new NotFound($"Користувача по Id: {request.Id} не знайдено");
            }

            if (_repository.IsAdminUserId(request.Id))
            {
                throw new BadRequestException(SD.SuperAdminChangeNotAllowed);
            }

            await _repository.DeleteAsync(user);
        }
    }
}
