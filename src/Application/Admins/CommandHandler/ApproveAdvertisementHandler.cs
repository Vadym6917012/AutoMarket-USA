using Application.Admins.Commands;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Admins.CommandHandler
{
    public class ApproveAdvertisementHandler : IRequestHandler<ApproveAdvertisement, bool>
    {
        private readonly IAdminRepository<ApplicationUser> _repository;

        public ApproveAdvertisementHandler(IAdminRepository<ApplicationUser> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(ApproveAdvertisement request, CancellationToken cancellationToken)
        {
            return await _repository.UpdateAdvertisementApprovalAsync(request.Id, request.IsApproved);
        }
    }
}
