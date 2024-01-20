using MediatR;

namespace Application.Accounts.Queries
{
    public class RefreshTokenQuery : IRequest<RefreshTokenResult>
    {
        public string Token { get; set; }
        public string UserId { get; set; }
    }
}
