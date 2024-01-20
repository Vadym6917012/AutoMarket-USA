using Application.DTOs.Account;
using Microsoft.AspNetCore.Identity;

namespace Application.Common.Interfaces
{
    public interface IAccountRepository
    {
        Task<ApplicationUserDTO> RefreshToken(string userId, string refreshToken);
        Task<SignInResult> CheckPasswordSignInAsync(ApplicationUser user, string password, bool lockoutOnFailure);
        Task<ApplicationUserDTO> CreateApplicationUserDTO(ApplicationUser user);
        Task<bool> SendConfirmEmailAsync(ApplicationUser user);
        Task<bool> SendForgotUsernameOrPassword(ApplicationUser user);
        Task<RefreshToken> SaveRefreshTokenAsync(ApplicationUser user);
        Task<bool> IsValidRefreshTokenAsync(string userId, string token);
    }
}
