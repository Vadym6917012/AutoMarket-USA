﻿using Domain.Constants;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Infrastructure.Data
{
    public class ContextSeedService
    {
        private readonly DataContext _ctx;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ContextSeedService(DataContext ctx,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _ctx = ctx;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task InitializeContextAsync()
        {
            if (_ctx.Database.GetPendingMigrationsAsync().GetAwaiter().GetResult().Count() > 0)
            {
                await _ctx.Database.MigrateAsync();
            }

            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = SD.AdminRole });
                await _roleManager.CreateAsync(new IdentityRole { Name = SD.ManagerRole });
                await _roleManager.CreateAsync(new IdentityRole { Name = SD.MemberRole });
            }

            if (!_userManager.Users.AnyAsync().GetAwaiter().GetResult())
            {
                var admin = new ApplicationUser
                {
                    FirstName = "admin",
                    LastName = "admin",
                    UserName = SD.AdminUserName,
                    Email = SD.AdminUserName,
                    EmailConfirmed = true
                };
                await _userManager.CreateAsync(admin, "adminPassword");
                await _userManager.AddToRolesAsync(admin, new[] { SD.AdminRole, SD.ManagerRole, SD.MemberRole });
                await _userManager.AddClaimsAsync(admin, new Claim[]
                {
                    new Claim(ClaimTypes.Email, admin.Email),
                    new Claim(ClaimTypes.Surname, admin.LastName)
                });

                var manager = new ApplicationUser
                {
                    FirstName = "manager",
                    LastName = "manager",
                    UserName = "manager@automarket.com",
                    NormalizedUserName = "manager@automarket.com".ToUpper(),
                    Email = "manager@automarket.com",
                    NormalizedEmail = "manager@automarket.com".ToUpper(),
                    EmailConfirmed = true
                };
                await _userManager.CreateAsync(manager, "managerPassword");
                await _userManager.AddToRoleAsync(manager, SD.ManagerRole);
                await _userManager.AddClaimsAsync(manager, new Claim[]
                {
                    new Claim(ClaimTypes.Email, manager.Email),
                    new Claim(ClaimTypes.Surname, manager.LastName)
                });

                var member = new ApplicationUser
                {
                    FirstName = "member",
                    LastName = "member",
                    UserName = "member@automarket.com",
                    NormalizedUserName = "member@automarket.com".ToUpper(),
                    Email = "memeber@automarket.com",
                    NormalizedEmail = "member@automarket.com".ToUpper(),
                    EmailConfirmed = true
                };
                await _userManager.CreateAsync(member, "memberPassword");
                await _userManager.AddToRoleAsync(member, SD.MemberRole);
                await _userManager.AddClaimsAsync(member, new Claim[]
                {
                    new Claim(ClaimTypes.Email, member.Email),
                    new Claim(ClaimTypes.Surname, member.LastName)
                });
            }
        }
    }
}
