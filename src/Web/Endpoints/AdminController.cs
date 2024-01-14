using Application.DTOs.Admin;
using Domain.Constants;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepository<ApplicationUser> _adminRepository;

        public AdminController(IAdminRepository<ApplicationUser> adminRepository)
        {
            _adminRepository = adminRepository;
        }

        [HttpGet("get-members")]
        public async Task<ActionResult<IEnumerable<MemberViewDto>>> GetMembers()
        {
            var members = await _adminRepository.GetMembersAsync();

            return Ok(members);
        }

        [HttpGet("get-member/{id}")]
        public async Task<ActionResult<MemberAddEditDto>> GetMember(string id)
        {
            var member = await _adminRepository.GetMemberAsync(id);

            if (member == null)
            {
                return NotFound();
            }

            return Ok(member);
        }

        [HttpPut("approve-advertisement")]
        public async Task<IActionResult> UpdateCar(int carId, bool isApproved)
        {
            var success = await _adminRepository.UpdateAdvertisementApprovalAsync(carId, isApproved);

            if (!success)
            {
                return NotFound();
            }

            return Ok(new JsonResult(new { title = "Оголошення підтвердженно успішно", message = "Оголошення було підтвердженно успішно", id = carId }));
        }

        [HttpPost("add-edit-member")]
        public async Task<IActionResult> AddEditMember(MemberAddEditDto model)
        {
            ApplicationUser user;

            if (string.IsNullOrEmpty(model.Id))
            {
                if (string.IsNullOrEmpty(model.Password) || model.Password.Length < 6)
                {
                    ModelState.AddModelError("errors", "Пароль повинен складатися зі шести або більше символів.");
                    return BadRequest(ModelState);
                }

                user = await _adminRepository.CreateAsync(model, model.Password);

                if (user == null)
                {
                    return BadRequest("Не вдалося створити користувача");
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(model.Password))
                {
                    if (model.Password.Length < 6)
                    {
                        ModelState.AddModelError("errors", "Пароль повинен складатися зі шести або більше символів.");
                        return BadRequest(ModelState);
                    }
                }

                if (_adminRepository.IsAdminUserId(model.Id))
                {
                    return BadRequest(SD.SuperAdminChangeNotAllowed);
                }

                user = await _adminRepository.FindByIdAsync(model.Id);
                if (user == null)
                {
                    return NotFound();
                }

                await _adminRepository.UpdateAsync(model.Id, model, model.Password);
            }

            var userRoles = await _adminRepository.GetRolesAsync(user);

            await _adminRepository.RemoveFromRolesAsync(user, userRoles);

            foreach (var role in model.Roles.Split(",").ToArray())
            {
                var roleToAdd = await _adminRepository.GetRolesAsync(role);
                if (roleToAdd != null)
                {
                    await _adminRepository.AddToRoleAsync(user, role);
                }
            }

            if (string.IsNullOrEmpty(model.Id))
            {
                return Ok(new JsonResult(new { title = "Користувач доданий", message = $"{model.UserName} створений" }));
            }
            else
            {
                return Ok(new JsonResult(new { title = "Користувач відредагований", message = $"{model.UserName} оновлений" }));
            }
        }

        [HttpDelete("delete-member/{id}")]
        public async Task<IActionResult> DeleteMember(string id)
        {
            var user = await _adminRepository.FindByIdAsync(id);
            if (user == null) return NotFound();

            if (_adminRepository.IsAdminUserId(id))
            {
                return BadRequest(SD.SuperAdminChangeNotAllowed);
            }

            await _adminRepository.DeleteAsync(user);
            return NoContent();
        }

        [HttpGet("get-application-roles")]
        public async Task<ActionResult<string[]>> GetApplicationRoles()
        {
            return Ok(await _adminRepository.GetApplicationRolesAsync());
        }
    }
}
