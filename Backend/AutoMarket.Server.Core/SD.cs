using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace AutoMarket.Server
{
    public static class SD
    {
        //Roles
        public const string AdminRole = "Admin";
        public const string ManagerRole = "Manager";
        public const string MemberRole = "Member";

        public const string AdminUserName = "admin@automarket.com";
        public const string SuperAdminChangeNotAllowed = "Super Admin change is not allowed!";
        public const int MaximumLoginAttempts = 3;

        public static bool VIPPolicy(AuthorizationHandlerContext context)
        {
            if (context.User.IsInRole(MemberRole) &&
                context.User.HasClaim(c => c.Type == ClaimTypes.Email && c.Value.Contains("vip")))
            {
                return true;
            }

            return false;
        }
    }
}
