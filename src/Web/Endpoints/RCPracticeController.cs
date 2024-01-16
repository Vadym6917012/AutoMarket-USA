using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class RCPracticeController : ControllerBase
    {
        [HttpGet("public")]
        public IActionResult Public()
        {
            return Ok("public");
        }

        #region Roles


        [HttpGet("admin-role")]
        [Authorize(Roles = "Admin")]
        public IActionResult AdminRole()
        {
            return Ok("admin role");
        }

        [HttpGet("manager-role")]
        [Authorize(Roles = "Manager")]
        public IActionResult ManagerRole()
        {
            return Ok("manager role");
        }

        [HttpGet("member-role")]
        [Authorize(Roles = "Member")]
        public IActionResult MemberRole()
        {
            return Ok("member role");
        }

        [HttpGet("admin-or-manager-role")]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult AdminOrManagerRole()
        {
            return Ok("admin or manager role");
        }

        [HttpGet("admin-or-member-role")]
        [Authorize(Roles = "Admin,Member")]
        public IActionResult AdminOrMemberRole()
        {
            return Ok("admin or member role");
        }
        #endregion

        #region Policy

        [HttpGet("admin-policy")]
        [Authorize(policy: "AdminPolicy")]
        public IActionResult AdminPolicy()
        {
            return Ok("admin policy");
        }

        [HttpGet("manager-policy")]
        [Authorize(policy: "ManagerPolicy")]
        public IActionResult ManagerPolicy()
        {
            return Ok("manager policy");
        }

        [HttpGet("member-policy")]
        [Authorize(policy: "MemberPolicy")]
        public IActionResult MemberPolicy()
        {
            return Ok("member policy");
        }


        [HttpGet("admin-or-manager-policy")]
        [Authorize(policy: "AdminOrManagerPolicy")]
        public IActionResult AdminOrManagerPolicy()
        {
            return Ok("admin or manager policy");
        }

        [HttpGet("admin-and-manager-policy")]
        [Authorize(policy: "AdminAndManagerPolicy")]
        public IActionResult AdminAndManagerPolicy()
        {
            return Ok("admin and manager policy");
        }

        [HttpGet("all-role-policy")]
        [Authorize(policy: "AllRolePolicy")]
        public IActionResult AllRolePolicy()
        {
            return Ok("all role policy");
        }

        #endregion

        #region Claim Policy

        [HttpGet("admin-email-policy")]
        [Authorize(policy: "AdminEmailPolicy")]
        public IActionResult AdminEmailPolicy()
        {
            return Ok("admin email policy");
        }

        [HttpGet("admin-surname-policy")]
        [Authorize(policy: "AdminSurnamePolicy")]
        public IActionResult MillerSurnamePolicy()
        {
            return Ok("miller surname policy");
        }

        [HttpGet("manager-email-and-manager-surname-policy")]
        [Authorize(policy: "ManagerEmailAndManagerSurnamePolicy")]
        public IActionResult ManagerEmailAndManagerSurnamePolicy()
        {
            return Ok("manager email and manager surname policy");
        }

        [HttpGet("vip-policy")]
        [Authorize(policy: "VIPPolicy")]
        public IActionResult VIPPolicy()
        {
            return Ok("vip policy");
        }

        #endregion
    }
}
