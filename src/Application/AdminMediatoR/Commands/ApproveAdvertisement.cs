using MediatR;

namespace Application.AdminMediatoR.Commands
{
    public class ApproveAdvertisement : IRequest<bool>
    {
        public int Id { get; set; }
        public bool IsApproved { get; set; }
    }
}
