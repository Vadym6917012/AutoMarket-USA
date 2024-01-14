using Application.DTOs.Admin;
using Microsoft.AspNetCore.Identity;

namespace Application.Common.Interfaces
{
    public interface IAdminRepository<T> where T : class
    {
        Task<IEnumerable<MemberViewDto>> GetMembersAsync();
        Task<MemberAddEditDto> GetMemberAsync(string userId);
        Task<bool> UpdateAdvertisementApprovalAsync(int carId, bool isApproved);
        bool IsAdminUserId(string userId);
        Task<T> FindByIdAsync(string userId);
        Task<IdentityResult> DeleteAsync(T user);
        Task<IEnumerable<string>> GetApplicationRolesAsync();
        Task<T> CreateAsync(MemberAddEditDto model, string password);
        Task<List<string>> GetRolesAsync(T user);
        Task<IdentityRole> GetRolesAsync(string role);
        Task<IdentityResult> RemoveFromRolesAsync(T user, List<string> userRoles);
        Task UpdateAsync(string userId, MemberAddEditDto model, string password);
        Task<IdentityResult> AddToRoleAsync(T user, string role);
    }
}
