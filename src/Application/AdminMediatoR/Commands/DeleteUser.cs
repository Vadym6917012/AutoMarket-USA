using MediatR;

namespace Application.AdminMediatoR.Commands
{
    public class DeleteUser : IRequest
    {
        public string Id { get; set; }
    }
}
