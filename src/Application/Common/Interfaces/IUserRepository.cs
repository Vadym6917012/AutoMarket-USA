using Application.DTOs.Account;
using Application.DTOs.Admin;
using Microsoft.AspNetCore.Identity;

namespace Application.Common.Interfaces
{
    public interface IUserRepository
    {
        Task<ApplicationUser> FindByIdAsync(string userId);
        Task<ApplicationUser> FindByEmailAsync(string email);
        Task<bool> CheckEmailConfirmedAsync(ApplicationUser user);
        Task AccessFailedAsync(ApplicationUser user);
        Task SetLockoutEndDateAsync(ApplicationUser user, DateTimeOffset? lockoutEnd);
        Task ResetAccessFailedCountAsync(ApplicationUser user);
        Task<List<string>> GetRolesAsync(ApplicationUser user);
        Task<IdentityResult> RemoveFromRolesAsync(ApplicationUser user, List<string> userRoles);
        Task<IdentityRole> GetRolesAsync(string role);
        Task<IdentityResult> AddToRoleAsync(ApplicationUser user, string role);
        Task<bool> CheckEmailExistAsync(string email);
        Task<IdentityResult> RemovePasswordAsync(ApplicationUser user);
        Task<IdentityResult> AddPasswordAsync(ApplicationUser user, string password);
        Task<ApplicationUser> CreateUserAsync(RegisterDTO model, string role);
        Task<bool> ConfirmEmailAsync(string email, string token);
        Task<MemberAddEditDto> GetUserAsync(string memberId);
        Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, string decodedToken, string newPassword);
        Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user);
        Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user);
        Task DeleteUserAsync(string userId);
    }
}
