using Application.Common.Interfaces;
using Application.DTOs.Account;
using Application.DTOs.Admin;
using Domain.Constants;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Text;

namespace Infrastructure.Identity
{
    public class AccountRepository : IAccountRepository<ApplicationUser, RefreshToken>
    {
        private readonly JWTServices _jwtService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly DataContext _context;
        private readonly IConfiguration _config;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountRepository(JWTServices jwtService,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IEmailService emailService,
            DataContext context,
            IConfiguration config,
            RoleManager<IdentityRole> roleManager)
        {
            _jwtService = jwtService;
            _signInManager = signInManager;
            _userManager = userManager;
            _emailService = emailService;
            _context = context;
            _config = config;
            _roleManager = roleManager;
        }

        public async Task<ApplicationUser> FindByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<ApplicationUserDTO> RefreshToken(string userId, string refreshToken)
        {
            if (IsValidRefreshTokenAsync(userId, refreshToken).GetAwaiter().GetResult())
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return null;
                }
                return await CreateApplicationUserDTO(user);
            }

            return null;
        }

        public async Task<ApplicationUser> FindByEmailAsync(string email)
        {
            var user = await _userManager.FindByNameAsync(email);

            return user;
        }

        public async Task<bool> CheckEmailConfirmedAsync(ApplicationUser user)
        {
            return user != null && await _userManager.IsEmailConfirmedAsync(user);
        }

        public async Task<Microsoft.AspNetCore.Identity.SignInResult> CheckPasswordSignInAsync(ApplicationUser user, string password, bool lockoutOnFailure)
        {
            return await _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure);
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

        public async Task<bool> CheckEmailExistAsync(string email)
        {
            return await _userManager.Users.AnyAsync(x => x.Email == email.ToLower());
        }

        public async Task<ApplicationUser> CreateUserAsync(RegisterUserRequest model, string role)
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
            var user = await FindByEmailAsync(email);
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

        public async Task<MemberAddEditDto> GetMemberAsync(string memberId)
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
        public async Task<IdentityResult> RemovePasswordAsync(ApplicationUser user)
        {
            return await _userManager.RemovePasswordAsync(user);
        }
        public async Task<IdentityResult> AddPasswordAsync(ApplicationUser user, string password)
        {
            return await _userManager.AddPasswordAsync(user, password);
        }

        public async Task<List<string>> GetRolesAsync(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            return roles.ToList();
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

        public async Task AddEditMemberAsync(MemberAddEditDto model)
        {

        }

        public async Task<ApplicationUserDTO> CreateApplicationUserDTO(ApplicationUser user)
        {
            await SaveRefreshTokenAsync(user);
            return new ApplicationUserDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                JWT = await _jwtService.CreateJWT(user),
            };
        }

        public async Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, string decodedToken, string newPassword)
        {
            return await _userManager.ResetPasswordAsync(user, decodedToken, newPassword);
        }

        public async Task<bool> SendConfirmEmailAsync(ApplicationUser user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            var url = $"{_config["JWT:ClientUrl"]}/{_config["Email:ConfirmEmailPath"]}?token={token}&email={user.Email}";

            var body = $"<p>Вітаємо! : {user.FirstName} {user.LastName}</p>" +
                "<p>Дякуємо за реєстрацію на нашому веб-сайті. Щоб завершити процес реєстрації та активувати ваш обліковий запис, будь ласка, підтвердьте свою електронну пошту, перейшовши за посиланням нижче:</p>" +
                $"<p><a href=\"{url}\">Натисни тут</a></p>" +
                "<p>Якщо ви не реєструвалися на нашому веб-сайті, проігноруйте цей лист.</p>" +
                "<p>Дякуємо за вибір нашого сервісу!</p>" +
                "<br><p>З найкращими побажаннями,</p>" +
                $"{_config["Email:ApplicationName"]}";

            var emailSend = new EmailSendDTO(to: user.Email, "Підтвердження електронної пошти", body);

            return await _emailService.SendEmailAsync(emailSend);
        }

        public async Task<bool> SendForgotUsernameOrPassword(ApplicationUser user)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            var url = $"{_config["JWT:ClientUrl"]}/{_config["Email:ResetPasswordPath"]}?token={token}&email={user.Email}";

            var body = $"<p>Привіт: {user.FirstName} {user.LastName}</p>" +
                $"<p>Email адреса: {user.UserName}</p>" +
                "<p>Ви отримали цей лист, оскільки ви (або хтось інший) виразили бажання відновити пароль для вашого облікового запису.</p>" +
                "<p>Для відновлення паролю перейдіть за посиланням нижче:</p>" +
                $"<p><a href=\"{url}\">Натисни тут</a></p>" +
                "<p>Якщо ви не намагалися відновити пароль, проігноруйте цей лист.</p>" +
                "<br><p>З найкращими побажаннями,</p>" +
                $"{_config["Email:ApplicationName"]}";

            var emailSend = new EmailSendDTO(to: user.Email, "Відновлення паролю", body);

            return await _emailService.SendEmailAsync(emailSend);
        }

        public async Task<RefreshToken> SaveRefreshTokenAsync(ApplicationUser user)
        {
            var refreshToken = _jwtService.CreateRefreshToken(user);

            var existingRefreshToken = await _context.RefreshTokens.SingleOrDefaultAsync(x => x.UserId == user.Id);
            if (existingRefreshToken != null)
            {
                existingRefreshToken.Token = refreshToken.Token;
                existingRefreshToken.DateCreatedUtc = refreshToken.DateCreatedUtc;
                existingRefreshToken.DateExpiresUtc = refreshToken.DateExpiresUtc;
            }
            else
            {
                user.RefreshTokens.Add(refreshToken);
            }

            await _context.SaveChangesAsync();

            return refreshToken;
        }

        public async Task<bool> IsValidRefreshTokenAsync(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token)) return false;

            var fetchedRefreshToken = await _context.RefreshTokens
                .FirstOrDefaultAsync(x => x.UserId == userId && x.Token == token);
            if (fetchedRefreshToken == null) return false;
            if (fetchedRefreshToken.IsExpired) return false;

            return true;
        }
    }
}
