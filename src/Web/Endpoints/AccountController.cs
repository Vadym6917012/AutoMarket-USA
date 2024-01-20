using Application.Accounts.Commands;
using Application.Accounts.Queries;
using Application.DTOs.Account;
using Application.DTOs.Admin;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text;

namespace Web.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet("refresh-token")]
        public async Task<ActionResult<ApplicationUserDTO>> RefreshToken()
        {
            var token = Request.Cookies["identityAppRefreshToken"];
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var query = new RefreshTokenQuery { Token = token, UserId = userId };
            var result = await _mediator.Send(query);

            if (result != null)
            {
                var cookieOptions = new CookieOptions
                {
                    Expires = result.DateExpiresUtc,
                    IsEssential = true,
                    HttpOnly = true,
                };

                Response.Cookies.Append("identityAppRefreshToken", result.RefreshedToken, cookieOptions);

                return result.ApplicationUserDTO;
            }

            return Unauthorized("Недійсний або застарілий токен. Будь ласка, спробуйте увійти знову.");
        }

        [Authorize]
        [HttpGet("refresh-page")]
        public async Task<ActionResult<ApplicationUserDTO>> RefreshPage()
        {
            var query = new RefreshPageQuery
            {
                Email = User.FindFirst(ClaimTypes.Email)?.Value
            };

            try
            {
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            var query = new LoginUserQuery
            {
                UserName = model.UserName,
                Password = model.Password,
            };

            try
            {
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO model)
        {
            try
            {
                await _mediator.Send(new RegisterUserCommand { RegistrationData = model });

                return Ok(new JsonResult(new { title = "Акаунт створено успішно", message = "Ваш акаунт створено успішно, підтвердіть, будь ласка, вашу email пошту" }));
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailDTO model)
        {
            var command = new ConfirmEmailCommand
            {
                Email = model.Email,
                Token = model.Token,
            };

            try
            {
                var result = await _mediator.Send(command);

                if (result == true)
                {
                    return Ok(new JsonResult(new { title = "Email підтверджений", message = "Ваш email підтверджений, увійдіть в особистий кабінет" }));
                }

                return BadRequest("Неправильний токен, спробуйте пізніше");
            }
            catch (UnauthorizedAccessException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-member/{id}")]
        public async Task<ActionResult<MemberAddEditDto>> GetMember(string id)
        {
            try
            {
                var entity = await _mediator.Send(new GetUserById { Id = id });

                return entity;
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost("edit-member")]
        public async Task<IActionResult> EditUser(MemberAddEditDto model)
        {
            if (!string.IsNullOrEmpty(model.Password))
            {
                if (model.Password.Length < 6)
                {
                    ModelState.AddModelError("errors", "Пароль повинен містити щонайменше 6 символів.");
                    return BadRequest(ModelState);
                }
            }

            var command = new UpdateUser
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Password = model.Password,
                PhoneNumber = model.PhoneNumber,
                Roles = model.Roles,
                UserName = model.UserName
            };

            await _mediator.Send(command);

            return Ok(new JsonResult(new { title = "Користувач відредагований", message = $"{model.UserName} оновлений" }));

        }

        [HttpPost("resend-email-confirmation-link/{email}")]
        public async Task<IActionResult> ResendEmailConfirmationLink(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Неправильний email");
            }

            var command = new ResendEmailConfirmationLinkCommand { Email = email };

            try
            {
                var result = await _mediator.Send(command);

                if (result == true)
                {
                    return Ok(new JsonResult(new { title = "Відправлено посилання для підтвердження.", message = "Підтвердіть вашу email адресу" }));
                }
                return BadRequest("Не вдалося відправити електронного листа.");
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost("forgot-username-or-password/{email}")]
        public async Task<IActionResult> ForgotUsernameOrPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Неправильний email");
            }

            var command = new ForgotUsernameOrPasswordCommand
            {
                Email = email
            };

            try
            {
                var result = await _mediator.Send(command);
                if (result == true)
                {
                    return Ok(new JsonResult(new { title = "Відправлено листа щодо забутого імені користувача або паролю.", message = "Перевірте ваш email" }));
                }
                return BadRequest("Не вдалося відправити електронного листа. Зверніться до адміністратора для отримання додаткової допомоги.");
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO model)
        {
            var decodedTokenBytes = WebEncoders.Base64UrlDecode(model.Token);
            var decodedToken = Encoding.UTF8.GetString(decodedTokenBytes);

            var command = new ResetPasswordCommand
            {
                Email = model.Email,
                Token = model.Token,
                NewPassword = model.NewPassword,
                DecodedToken = decodedToken
            };

            try
            {
                var result = await _mediator.Send(command);

                if (result == true)
                {
                    return Ok(new JsonResult(new { title = "Пароль змінено успішно", message = "Ваш пароль було змінено" }));
                }
                return BadRequest("Неправильний токен, спробуйте пізніше");
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
