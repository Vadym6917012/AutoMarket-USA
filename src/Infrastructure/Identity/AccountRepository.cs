using Application.Common.Interfaces;
using Application.DTOs.Account;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace Infrastructure.Identity
{
    public class AccountRepository : IAccountRepository
    {
        private readonly JWTServices _jwtService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly DataContext _context;
        private readonly IConfiguration _config;
        private readonly IUserRepository _repository;

        public AccountRepository(JWTServices jwtService,
            SignInManager<ApplicationUser> signInManager,
            IEmailService emailService,
            DataContext context,
            IConfiguration config,
            IUserRepository repository)
        {
            _jwtService = jwtService;
            _signInManager = signInManager;
            _emailService = emailService;
            _context = context;
            _config = config;
            _repository = repository;
        }

        public async Task<ApplicationUserDTO> RefreshToken(string userId, string refreshToken)
        {
            if (IsValidRefreshTokenAsync(userId, refreshToken).GetAwaiter().GetResult())
            {
                var user = await _repository.FindByIdAsync(userId);
                if (user == null)
                {
                    return null;
                }
                return await CreateApplicationUserDTO(user);
            }

            return null;
        }

        public async Task<SignInResult> CheckPasswordSignInAsync(ApplicationUser user, string password, bool lockoutOnFailure)
        {
            return await _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure);
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

        public async Task<bool> SendConfirmEmailAsync(ApplicationUser user)
        {
            var token = await _repository.GenerateEmailConfirmationTokenAsync(user);
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            var url = $"{_config["JWT:ClientUrl"]}/{_config["Email:ConfirmEmailPath"]}?token={token}&email={user.Email}";

            var body = $"<p>Вітаємо! : {user.FirstName} {user.LastName}</p>" +
                "<p>Дякуємо за реєстрацію на нашому веб-сайті. Щоб завершити процес реєстрації та активувати ваш обліковий запис, будь ласка, підтвердьте свою електронну пошту, перейшовши за посиланням нижче:</p>" +
                $"<p><a href=\"{url}\">Натисни тут</a></p>" +
                "<p>Якщо ви не реєструвалися на нашому веб-сайті, проігноруйте цей лист.</p>" +
                "<p>Дякуємо за вибір нашого сервісу!</p>";

            var emailSend = new EmailSendDTO(to: user.Email, "Підтвердження електронної пошти", body);

            return await _emailService.SendEmailAsync(emailSend);
        }

        public async Task<bool> SendForgotUsernameOrPassword(ApplicationUser user)
        {
            var token = await _repository.GeneratePasswordResetTokenAsync(user);
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            var url = $"{_config["JWT:ClientUrl"]}/{_config["Email:ResetPasswordPath"]}?token={token}&email={user.Email}";

            var body = $"<p>Привіт: {user.FirstName} {user.LastName}</p>" +
                $"<p>Email адреса: {user.UserName}</p>" +
                "<p>Ви отримали цей лист, оскільки ви (або хтось інший) виразили бажання відновити пароль для вашого облікового запису.</p>" +
                "<p>Для відновлення паролю перейдіть за посиланням нижче:</p>" +
                $"<p><a href=\"{url}\">Натисни тут</a></p>" +
                "<p>Якщо ви не намагалися відновити пароль, проігноруйте цей лист.</p>";

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
