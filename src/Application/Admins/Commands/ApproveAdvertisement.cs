using MediatR;

namespace Application.Admins.Commands
{
    public class ApproveAdvertisement : IRequest<bool>
    {
        public int Id { get; set; }
        public bool IsApproved { get; set; }
    }
}
