using Application.DTOs.Account;
using Application.DTOs.Admin;
using Microsoft.AspNetCore.Identity;

namespace Application.Common.Interfaces
{
    public interface IAccountRepository<T, R> where T : class
    {
        Task<ApplicationUserDTO> RefreshToken(string userId, string refreshToken);
        Task<T> FindByEmailAsync(string email);
        Task<T> FindByIdAsync(string id);
        Task<ApplicationUserDTO> CreateApplicationUserDTO(T user);
        Task<SignInResult> CheckPasswordSignInAsync(T user, string password, bool lockoutOnFailure);
        Task AccessFailedAsync(T user);
        Task SetLockoutEndDateAsync(T user, DateTimeOffset? lockoutEnd);
        Task ResetAccessFailedCountAsync(T user);
        Task<bool> CheckEmailExistAsync(string email);
        Task<T> CreateUserAsync(RegisterUserRequest model, string role);
        Task<bool> SendConfirmEmailAsync(T user);
        Task<bool> ConfirmEmailAsync(string email, string token);
        Task<MemberAddEditDto> GetMemberAsync(string memberId);
        Task AddEditMemberAsync(MemberAddEditDto model);
        Task<IdentityResult> RemovePasswordAsync(T user);
        Task<IdentityResult> AddPasswordAsync(T user, string password);
        Task<List<string>> GetRolesAsync(T user);
        Task<IdentityResult> RemoveFromRolesAsync(T user, List<string> userRoles);
        Task<IdentityRole> GetRolesAsync(string role);
        Task<IdentityResult> AddToRoleAsync(T user, string role);
        Task<IdentityResult> ResetPasswordAsync(T user, string decodedToken, string newPassword);
        Task<bool> SendForgotUsernameOrPassword(T user);
        Task<bool> IsValidRefreshTokenAsync(string userId, string token);
        Task<R> SaveRefreshTokenAsync(T user);
    }
}
