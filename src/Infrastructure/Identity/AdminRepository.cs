using Application.Common.Interfaces;
using Application.DTOs.Account;
using Application.DTOs.Admin;
using Domain.Constants;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Identity
{
    public class AdminRepository : IAdminRepository<ApplicationUser>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ICarRepository _carRepository;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _config;

        public AdminRepository(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ICarRepository carRepository,
            IEmailService emailService,
            IConfiguration config)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _carRepository = carRepository;
            _emailService = emailService;
            _config = config;
        }

        public async Task<IEnumerable<MemberViewDto>> GetMembersAsync()
        {
            List<MemberViewDto> members = new List<MemberViewDto>();

            var users = await _userManager.Users
                .Where(x => x.UserName != SD.AdminUserName)
                .ToListAsync();

            foreach (var user in users)
            {
                var memberToAdd = new MemberViewDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    DateCreated = user.DateCreated,
                    Roles = await _userManager.GetRolesAsync(user),
                };

                members.Add(memberToAdd);
            }

            return members;
        }

        public async Task<MemberAddEditDto> GetMemberAsync(string userId)
        {
            var user = await _userManager.Users
                .Where(x => x.UserName != SD.AdminUserName && x.Id == userId)
                .FirstOrDefaultAsync();

            var member = new MemberAddEditDto
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Roles = string.Join(",", await _userManager.GetRolesAsync(user))
            };

            return member;
        }

        public async Task<bool> UpdateAdvertisementApprovalAsync(int carId, bool isApproved)
        {
            var existingEntity = _carRepository.GetById(carId);

            if (existingEntity == null)
            {
                return false;
            }

            existingEntity.IsAdvertisementApproved = isApproved;

            await _carRepository.UpdateAsync(existingEntity);

            var user = await _userManager.Users
                .Where(x => x.Id == existingEntity.UserId)
                .FirstOrDefaultAsync();

            await SendEmailForUser(user, isApproved);

            await _carRepository.SaveChangesAsync();

            return true;
        }

        public async Task<ApplicationUser> FindByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public bool IsAdminUserId(string userId)
        {
            return _userManager.FindByIdAsync(userId).GetAwaiter().GetResult().UserName.Equals(SD.AdminUserName);
        }

        public async Task<IdentityResult> DeleteAsync(ApplicationUser user)
        {
            return await _userManager.DeleteAsync(user);
        }

        public async Task<IEnumerable<string>> GetApplicationRolesAsync()
        {
            var roles = await _roleManager.Roles.Select(x => x.Name).ToListAsync();
            return roles;
        }

        public async Task<ApplicationUser> CreateAsync(MemberAddEditDto model, string password)
        {
            var user = new ApplicationUser
            {
                FirstName = model.FirstName.ToLower(),
                LastName = model.LastName.ToLower(),
                UserName = model.UserName.ToLower(),
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, password);
            return result.Succeeded ? user : null;
        }

        public async Task UpdateAsync(string userId, MemberAddEditDto model, string password)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null || IsAdminUserId(userId))
            {
                return;
            }

            user.FirstName = model.FirstName.ToLower();
            user.LastName = model.LastName.ToLower();
            user.UserName = model.UserName.ToLower();

            if (!string.IsNullOrEmpty(password))
            {
                await _userManager.RemovePasswordAsync(user);
                await _userManager.AddPasswordAsync(user, password);
            }

            await _userManager.UpdateAsync(user);
        }

        private async Task<bool> SendEmailForUser(ApplicationUser user, bool isApproved)
        {
            var body = "";

            if (isApproved == true)
            {
                body = $"<p>Вітаємо! : {user.FirstName} {user.LastName}</p>" +
                "<p>Ваше оголошення було підтвердженно:</p>" +
                "<p>Дякуємо за вибір нашого сервісу!</p>" +
                "<br><p>З найкращими побажаннями,</p>" +
                $"{_config["Email:ApplicationName"]}";
            }
            else if (isApproved == false)
            {
                body = $"<p>Вітаємо! : {user.FirstName} {user.LastName}</p>" +
                "<p>Ваше оголошення було відхилено:</p>" +
                "<p>Дякуємо за вибір нашого сервісу!</p>" +
                "<br><p>З найкращими побажаннями,</p>" +
                $"{_config["Email:ApplicationName"]}";
            }

            var emailSend = new EmailSendDTO(to: user.Email, "Підтвердження статусу оголошення", body);

            return await _emailService.SendEmailAsync(emailSend);
        }
        public async Task<List<string>> GetRolesAsync(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            return roles.ToList();
        }

        public async Task<IdentityRole> GetRolesAsync(string role)
        {
            return await _roleManager.Roles.FirstOrDefaultAsync(r => r.Name == role);
        }

        public async Task<IdentityResult> RemoveFromRolesAsync(ApplicationUser user, List<string> userRoles)
        {
            return await _userManager.RemoveFromRolesAsync(user, userRoles);
        }

        public async Task<IdentityResult> AddToRoleAsync(ApplicationUser user, string role)
        {
            return await _userManager.AddToRoleAsync(user, role);
        }
    }
}
