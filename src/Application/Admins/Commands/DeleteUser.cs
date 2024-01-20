using MediatR;

namespace Application.Admins.Commands
{
    public class DeleteUser : IRequest
    {
        public string Id { get; set; }
    }
}
