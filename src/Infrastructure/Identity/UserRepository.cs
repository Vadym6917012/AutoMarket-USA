using Application.Common.Interfaces;
using Application.DTOs.Account;
using Application.DTOs.Admin;
using Domain.Constants;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Text;

public class UserRepository : IUserRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserRepository(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
    }

    public async Task<ApplicationUser> FindByIdAsync(string userId)
    {
        return await _userManager.FindByIdAsync(userId);
    }

    public async Task<ApplicationUser> FindByEmailAsync(string email)
    {
        return await _userManager.FindByNameAsync(email);
    }

    public async Task<bool> CheckEmailConfirmedAsync(ApplicationUser user)
    {
        return user != null && await _userManager.IsEmailConfirmedAsync(user);
    }

    public async Task AccessFailedAsync(ApplicationUser user)
    {
        await _userManager.AccessFailedAsync(user);
    }

    public async Task SetLockoutEndDateAsync(ApplicationUser user, DateTimeOffset? lockoutEnd)
    {
        await _userManager.SetLockoutEndDateAsync(user, lockoutEnd);
    }

    public async Task ResetAccessFailedCountAsync(ApplicationUser user)
    {
        await _userManager.ResetAccessFailedCountAsync(user);
    }

    public async Task<List<string>> GetRolesAsync(ApplicationUser user)
    {
        return (await _userManager.GetRolesAsync(user)).ToList();
    }

    public async Task<IdentityResult> RemoveFromRolesAsync(ApplicationUser user, List<string> userRoles)
    {
        return await _userManager.RemoveFromRolesAsync(user, userRoles);
    }

    public async Task<IdentityRole> GetRolesAsync(string role)
    {
        return await _roleManager.Roles.FirstOrDefaultAsync(r => r.Name == role);
    }

    public async Task<IdentityResult> AddToRoleAsync(ApplicationUser user, string role)
    {
        return await _userManager.AddToRoleAsync(user, role);
    }

    public async Task<bool> CheckEmailExistAsync(string email)
    {
        return await _userManager.Users.AnyAsync(x => x.Email == email.ToLower());
    }

    public async Task<IdentityResult> RemovePasswordAsync(ApplicationUser user)
    {
        return await _userManager.RemovePasswordAsync(user);
    }

    public async Task<IdentityResult> AddPasswordAsync(ApplicationUser user, string password)
    {
        return await _userManager.AddPasswordAsync(user, password);
    }

    public async Task<ApplicationUser> CreateUserAsync(RegisterDTO model, string role)
    {
        var userToAdd = new ApplicationUser
        {
            FirstName = model.FirstName.ToLower(),
            LastName = model.LastName.ToLower(),
            UserName = model.Email.ToLower(),
            Email = model.Email.ToLower(),
        };

        var result = await _userManager.CreateAsync(userToAdd, model.Password);
        if (!result.Succeeded)
        {
            throw new ApplicationException($"Не вдалося створити юзера: {string.Join(", ", result.Errors)}");
        }

        await _userManager.AddToRoleAsync(userToAdd, role);

        return userToAdd;
    }

    public async Task<bool> ConfirmEmailAsync(string email, string token)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return false;
        }

        try
        {
            var decodedTokenBytes = WebEncoders.Base64UrlDecode(token);
            var decodedToken = Encoding.UTF8.GetString(decodedTokenBytes);

            var result = await _userManager.ConfirmEmailAsync(user, decodedToken);
            return result.Succeeded;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<MemberAddEditDto> GetUserAsync(string memberId)
    {
        var user = await _userManager.Users
        .Where(x => x.UserName != SD.AdminUserName && x.Id == memberId)
        .FirstOrDefaultAsync();

        if (user == null)
        {
            return null;
        }

        return new MemberAddEditDto
        {
            Id = user.Id,
            UserName = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
            Roles = string.Join(",", await _userManager.GetRolesAsync(user))
        };
    }

    public async Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, string decodedToken, string newPassword)
    {
        return await _userManager.ResetPasswordAsync(user, decodedToken, newPassword);
    }

    public async Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user)
    {
        return await _userManager.GenerateEmailConfirmationTokenAsync(user);
    }

    public async Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user)
    {
        return await _userManager.GeneratePasswordResetTokenAsync(user);
    }

    public async Task DeleteUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user != null)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, userRoles);
        }
    }
}