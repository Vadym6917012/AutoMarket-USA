using Application.AccountMediatoR.Queries;
using Application.Common.Interfaces;
using Application.DTOs.Admin;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.AccountMediatoR.QueryHandler
{
    public class GetUserByIdHandler : IRequestHandler<GetUserById, MemberAddEditDto>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<MemberAddEditDto> Handle(GetUserById request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserAsync(request.Id);

            if (user == null)
            {
                throw new ValidationException($"Користувача по ID: {request.Id} не знайдено");
            }

            return user;
        }
    }
}
