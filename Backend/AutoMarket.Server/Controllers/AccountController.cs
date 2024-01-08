using AutoMarket.Server.Core;
using AutoMarket.Server.Core.Models;
using AutoMarket.Server.Infrastructure;
using AutoMarket.Server.Shared.DTOs.Account;
using AutoMarket.Server.Shared.DTOs.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text;

namespace AutoMarket.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JWTServices _jwtService;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly EmailService _emailService;
        private readonly DataContext _context;
        private readonly IConfiguration _config;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(JWTServices jwtService,
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            EmailService emailService,
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

        [Authorize]
        [HttpGet("refresh-token")]
        public async Task<ActionResult<UserDTO>> RefreshToken()
        {
            var token = Request.Cookies["identityAppRefreshToken"];
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (IsValidRefreshTokenAsync(userId, token).GetAwaiter().GetResult())
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null) return Unauthorized("Недійсний або застарілий токен. Будь ласка, спробуйте увійти знову.");
                return await CreateApplicationUserDTO(user);
            }

            return Unauthorized("Недійсний або застарілий токен. Будь ласка, спробуйте увійти знову.");
        }

        [Authorize]
        [HttpGet("refresh-page")]
        public async Task<ActionResult<UserDTO>> RefreshPage()
        {
            var user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.Email)?.Value);

            return await CreateApplicationUserDTO(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                return Unauthorized("Неправильне ім`я або пароль");
            }

            if (user.EmailConfirmed == false)
            {
                return Unauthorized("Підтвердіть ваш email.");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded)
            {
                if (!user.UserName.Equals(SD.AdminUserName))
                {
                    await _userManager.AccessFailedAsync(user);
                }

                if (user.AccessFailedCount >= SD.MaximumLoginAttempts)
                {
                    await _userManager.SetLockoutEndDateAsync(user, DateTime.UtcNow.AddDays(1));
                    return Unauthorized(string.Format("Ваш акаунт було заблоковано. Вам потрібно зачекати до {0} щоб залогінитися", user.LockoutEnd));
                }
                return Unauthorized("Неправильне ім`я або пароль");
            }

            await _userManager.ResetAccessFailedCountAsync(user);
            await _userManager.SetLockoutEndDateAsync(user, null);

            return await CreateApplicationUserDTO(user);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO model)
        {
            if (await CheckEmailExistAsync(model.Email))
            {
                return BadRequest($"Існуючий акаунт вже є під таким email: {model.Email}");
            }

            var userToAdd = new User
            {
                FirstName = model.FirstName.ToLower(),
                LastName = model.LastName.ToLower(),
                UserName = model.Email.ToLower(),
                Email = model.Email.ToLower(),
            };

            var result = await _userManager.CreateAsync(userToAdd, model.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            await _userManager.AddToRoleAsync(userToAdd, SD.MemberRole);

            try
            {
                if (await SendConfirmEmailAsync(userToAdd))
                {
                    return Ok(new JsonResult(new { title = "Акаунт створено успішно", message = "Ваш акаунт створено успішно, підтвердіть, будь ласка, вашу email пошту" }));
                }

                return BadRequest("Не вдалося відправити електронного листа. Зверніться до адміністратора для отримання додаткової допомоги.");
            }
            catch (Exception)
            {

                return BadRequest("Не вдалося відправити електронного листа. Зверніться до адміністратора для отримання додаткової допомоги.");
            }
        }

        [HttpPut("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return Unauthorized("Email адреса ще не зареєстрована");
            }

            if (user.EmailConfirmed == true)
            {
                return BadRequest("Ваш email вже підтверджений, увійдіть в особистий кабінет");
            }

            try
            {
                var decodedTokenBytes = WebEncoders.Base64UrlDecode(model.Token);
                var decodedToken = Encoding.UTF8.GetString(decodedTokenBytes);

                var result = await _userManager.ConfirmEmailAsync(user, decodedToken);
                if (result.Succeeded)
                {
                    return Ok(new JsonResult(new { title = "Email підтверджений", message = "Ваш email підтверджений, увійдіть в особистий кабінет" }));
                }

                return BadRequest("Неправильний токен, спробуйте пізніше");
            }
            catch (Exception)
            {
                return BadRequest("Неправильний токен, спробуйте пізніше");
            }
        }

        [HttpGet("get-member/{id}")]
        public async Task<ActionResult<MemberAddEditDto>> GetMember(string id)
        {
            var user = await _userManager.Users
                .Where(x => x.UserName != SD.AdminUserName && x.Id == id)
                .FirstOrDefaultAsync();

            var member = new MemberAddEditDto
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Roles = string.Join(",", await _userManager.GetRolesAsync(user))
            };

            return Ok(member);
        }

        [HttpPost("add-edit-member")]
        public async Task<IActionResult> AddEditMember(MemberAddEditDto model)
        {
            User user;

            if (!string.IsNullOrEmpty(model.Password))
            {
                if (model.Password.Length < 6)
                {
                    ModelState.AddModelError("errors", "Пароль повинен містити щонайменше 6 символів.");
                    return BadRequest(ModelState);
                }
            }

            user = await _userManager.FindByIdAsync(model.Id);
            if (user == null) return NotFound();

            user.FirstName = model.FirstName.ToLower();
            user.LastName = model.LastName.ToLower();
            user.UserName = model.UserName.ToLower();
            user.PhoneNumber = model.PhoneNumber.ToLower();

            if (!string.IsNullOrEmpty(model.Password))
            {
                await _userManager.RemovePasswordAsync(user);
                await _userManager.AddPasswordAsync(user, model.Password);
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            await _userManager.RemoveFromRolesAsync(user, userRoles);

            foreach (var role in model.Roles.Split(",").ToArray())
            {
                var roleToAdd = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Name == role);
                if (roleToAdd != null)
                {
                    await _userManager.AddToRoleAsync(user, role);
                }
            }

            return Ok(new JsonResult(new { title = "Користувач відредагований", message = $"{model.UserName} оновлений" }));

        }


        [HttpPost("resend-email-confirmation-link/{email}")]
        public async Task<IActionResult> ResendEmailConfirmationLink(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Неправильний email");
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return Unauthorized("Цей email ще не був зареєстрований");
            }
            if (user.EmailConfirmed == true)
            {
                return BadRequest("Ваш email вже підтверджений, увійдіть в особистий кабінет");
            }

            try
            {
                if (await SendConfirmEmailAsync(user))
                {
                    return Ok(new JsonResult(new { title = "Відправлено посилання для підтвердження.", message = "Підтвердіть вашу email адресу" }));
                }
                return BadRequest("Не вдалося відправити електронного листа.");
            }
            catch (Exception)
            {
                return BadRequest("Не вдалося відправити електронного листа.");
            }
        }

        [HttpPost("forgot-username-or-password/{email}")]
        public async Task<IActionResult> ForgotUsernameOrPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Неправильний email");
            }

            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return Unauthorized("Такий email ще не зареєстрований");
            }

            if (user.EmailConfirmed == false)
            {
                return BadRequest("Підтвердіть спочатку ваш email.");
            }

            try
            {
                if (await SendForgotUsernameOrPassword(user))
                {
                    return Ok(new JsonResult(new { title = "Відправлено листа щодо забутого імені користувача або паролю.", message = "Перевірте ваш email" }));
                }

                return BadRequest("Не вдалося відправити електронного листа. Зверніться до адміністратора для отримання додаткової допомоги.");
            }
            catch (Exception)
            {
                return BadRequest("Не вдалося відправити електронного листа. Зверніться до адміністратора для отримання додаткової допомоги.");
            }
        }

        [HttpPut("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return Unauthorized("Такий email ще не зареєстрований");
            }

            if (user.EmailConfirmed == false)
            {
                return BadRequest("Підтвердіть спочатку ваш email.");
            }

            try
            {
                var decodedTokenBytes = WebEncoders.Base64UrlDecode(model.Token);
                var decodedToken = Encoding.UTF8.GetString(decodedTokenBytes);

                var result = await _userManager.ResetPasswordAsync(user, decodedToken, model.NewPassword);
                if (result.Succeeded)
                {
                    return Ok(new JsonResult(new { title = "Пароль змінено успішно", message = "Ваш пароль було змінено" }));
                }

                return BadRequest("Неправильний токен, спробуйте пізніше");
            }
            catch (Exception)
            {
                return BadRequest("Неправильний токен, спробуйте пізніше");
            }
        }


        #region Private Helper Methods

        private async Task<UserDTO> CreateApplicationUserDTO(User user)
        {
            await SaveRefreshTokenAsync(user);
            return new UserDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                JWT = await _jwtService.CreateJWT(user),
            };
        }

        private async Task<bool> CheckEmailExistAsync(string email)
        {
            return await _userManager.Users.AnyAsync(x => x.Email == email.ToLower());
        }

        private async Task<bool> SendConfirmEmailAsync(User user)
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

        private async Task<bool> SendForgotUsernameOrPassword(User user)
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

        private async Task SaveRefreshTokenAsync(User user)
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

            var cookieOptions = new CookieOptions
            {
                Expires = refreshToken.DateExpiresUtc,
                IsEssential = true,
                HttpOnly = true,
            };

            Response.Cookies.Append("identityAppRefreshToken", refreshToken.Token, cookieOptions);
        }

        private async Task<bool> IsValidRefreshTokenAsync(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token)) return false;

            var fetchedRefreshToken = await _context.RefreshTokens
                .FirstOrDefaultAsync(x => x.UserId == userId && x.Token == token);
            if (fetchedRefreshToken == null) return false;
            if (fetchedRefreshToken.IsExpired) return false;

            return true;
        }

        public static string CreateHtmlEmail(string content)
        {
            
            string htmlBody = $@"";

            return htmlBody;
        }

        #endregion
    }
}
