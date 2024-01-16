using Application.DTOs.Account;
using Application.DTOs.Admin;
using Domain.Constants;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Security.Claims;
using System.Text;

namespace Web.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository<ApplicationUser, RefreshToken> _accountRepository;

        public AccountController(IAccountRepository<ApplicationUser, RefreshToken> accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [Authorize]
        [HttpGet("refresh-token")]
        public async Task<ActionResult<ApplicationUserDTO>> RefreshToken()
        {
            var token = Request.Cookies["identityAppRefreshToken"];
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (_accountRepository.IsValidRefreshTokenAsync(userId, token).GetAwaiter().GetResult())
            {
                var user = await _accountRepository.FindByIdAsync(userId);

                if (user == null) return Unauthorized("Недійсний або застарілий токен. Будь ласка, спробуйте увійти знову.");

                var refreshToken = await _accountRepository.SaveRefreshTokenAsync(user);

                var cookieOptions = new CookieOptions
                {
                    Expires = refreshToken.DateExpiresUtc,
                    IsEssential = true,
                    HttpOnly = true,
                };

                Response.Cookies.Append("identityAppRefreshToken", refreshToken.Token, cookieOptions);
            }

            var result = await _accountRepository.RefreshToken(userId, token);

            if (result != null)
            {
                return result;
            }

            return Unauthorized("Недійсний або застарілий токен. Будь ласка, спробуйте увійти знову.");
        }

        [Authorize]
        [HttpGet("refresh-page")]
        public async Task<ActionResult<ApplicationUserDTO>> RefreshPage()
        {
            var user = await _accountRepository.FindByEmailAsync(User.FindFirst(ClaimTypes.Email)?.Value);

            return await _accountRepository.CreateApplicationUserDTO(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ApplicationUserDTO>> Login(LoginUserRequest model)
        {
            var user = await _accountRepository.FindByEmailAsync(model.UserName);
            if (user == null)
            {
                return Unauthorized("Неправильне ім`я або пароль");
            }

            if (user.EmailConfirmed == false)
            {
                return Unauthorized("Підтвердіть ваш email.");
            }

            var result = await _accountRepository.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded)
            {
                if (!user.UserName.Equals(SD.AdminUserName))
                {
                    await _accountRepository.AccessFailedAsync(user);
                }

                if (user.AccessFailedCount >= SD.MaximumLoginAttempts)
                {
                    await _accountRepository.SetLockoutEndDateAsync(user, DateTime.UtcNow.AddDays(1));
                    return Unauthorized(string.Format("Ваш акаунт було заблоковано. Вам потрібно зачекати до {0} щоб залогінитися", user.LockoutEnd));
                }
                return Unauthorized("Неправильне ім`я або пароль");
            }

            await _accountRepository.ResetAccessFailedCountAsync(user);
            await _accountRepository.SetLockoutEndDateAsync(user, null);

            return await _accountRepository.CreateApplicationUserDTO(user);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserRequest model)
        {
            if (await _accountRepository.CheckEmailExistAsync(model.Email))
            {
                return BadRequest($"Існуючий акаунт вже є під таким email: {model.Email}");
            }

            var user = await _accountRepository.CreateUserAsync(model, SD.MemberRole);

            try
            {
                if (await _accountRepository.SendConfirmEmailAsync(user))
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
            var user = await _accountRepository.FindByEmailAsync(model.Email);
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
                if (await _accountRepository.ConfirmEmailAsync(model.Email, model.Token))
                {
                    return Ok(new JsonResult(new { title = "Email підтверджений", message = "Ваш email підтверджений, увійдіть в особистий кабінет" }));
                }

                return BadRequest("Неправильний токен, спробуйте пізніше");
            }
            catch (Exception ex)
            {
                return BadRequest($"Помилка підтвердження email, спробуйте пізніше: {ex.Message}");
            }
        }

        [HttpGet("get-member/{id}")]
        public async Task<ActionResult<MemberAddEditDto>> GetMember(string id)
        {
            var user = _accountRepository.GetMemberAsync(id);

            if (user == null)
            {
                return NotFound($"Користувача по ID: {id} не знайдено");
            }

            return Ok(user);
        }

        [HttpPost("add-edit-member")]
        public async Task<IActionResult> AddEditMember(MemberAddEditDto model)
        {
            ApplicationUser user;

            if (!string.IsNullOrEmpty(model.Password))
            {
                if (model.Password.Length < 6)
                {
                    ModelState.AddModelError("errors", "Пароль повинен містити щонайменше 6 символів.");
                    return BadRequest(ModelState);
                }
            }

            user = await _accountRepository.FindByIdAsync(model.Id);
            if (user == null) return NotFound();

            user.FirstName = model.FirstName.ToLower();
            user.LastName = model.LastName.ToLower();
            user.UserName = model.UserName.ToLower();
            user.PhoneNumber = model.PhoneNumber.ToLower();

            if (!string.IsNullOrEmpty(model.Password))
            {
                await _accountRepository.RemovePasswordAsync(user);
                await _accountRepository.AddPasswordAsync(user, model.Password);
            }

            var userRoles = await _accountRepository.GetRolesAsync(user);

            await _accountRepository.RemoveFromRolesAsync(user, userRoles);

            foreach (var role in model.Roles.Split(",").ToArray())
            {
                var roleToAdd = await _accountRepository.GetRolesAsync(role);
                if (roleToAdd != null)
                {
                    await _accountRepository.AddToRoleAsync(user, role);
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

            var user = await _accountRepository.FindByEmailAsync(email);
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
                if (await _accountRepository.SendConfirmEmailAsync(user))
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

            var user = await _accountRepository.FindByEmailAsync(email);

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
                if (await _accountRepository.SendForgotUsernameOrPassword(user))
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
            var user = await _accountRepository.FindByEmailAsync(model.Email);
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

                var result = await _accountRepository.ResetPasswordAsync(user, decodedToken, model.NewPassword);
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
    }
}
