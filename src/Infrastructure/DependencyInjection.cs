using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Identity;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectingString = configuration.GetConnectionString("AutoMarketConnection");

            services.AddDbContext<DataContext>(option =>
            {
                option.UseSqlServer(connectingString);
            });

            services.AddScoped<JWTServices>();
            services.AddScoped<IRepository<BodyType>, Repository<BodyType>>();
            services.AddScoped<IModelRepository,ModelRepository>();
            services.AddScoped<IGenerationRepository,GenerationRepository>();
            services.AddScoped<IMakeRepository,MakeRepository>();
            services.AddScoped<IRepository<FuelType>, Repository<FuelType>>();
            services.AddScoped<IRepository<GearBoxType>, Repository<GearBoxType>>();
            services.AddScoped<IModificationRepository,ModificationRepository>();
            services.AddScoped<ICarRepository,CarRepository>();
            services.AddScoped<IImagesRepository<IFormFile>,ImagesRepository>();
            services.AddScoped<IEmailService,EmailService>();
            services.AddScoped<IRepository<ProducingCountry>,ProducingCountryRepository>();
            services.AddScoped<ContextSeedService>();
            services.AddScoped<IRepository<DriveTrain>,Repository<DriveTrain>>();
            services.AddScoped<IRepository<TechnicalCondition>,Repository<TechnicalCondition>>();
            services.AddScoped<IAccountRepository<ApplicationUser, RefreshToken>, AccountRepository>();
            services.AddScoped<IAdminRepository<ApplicationUser>, AdminRepository>();

            // Налаштування IdentityCore юзера
            services.AddIdentityCore<ApplicationUser>(option =>
            {
                // Налаштування пароля
                option.Password.RequiredLength = 6;
                option.Password.RequireDigit = false;
                option.Password.RequireLowercase = false;
                option.Password.RequireUppercase = false;
                option.Password.RequireNonAlphanumeric = false;

                // Налаштування Email
                option.SignIn.RequireConfirmedEmail = true;
            })
                .AddRoles<IdentityRole>()
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddEntityFrameworkStores<DataContext>()
                .AddSignInManager<SignInManager<ApplicationUser>>()
                .AddUserManager<UserManager<ApplicationUser>>()
                .AddDefaultTokenProviders();

            // Додавання авторизації використовуючи JWT
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(option =>
                {
                    option.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"])),
                        ValidIssuer = configuration["JWT:Issuer"],
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
                opt.AddPolicy("ManagerPolicy", policy => policy.RequireRole("Manager"));
                opt.AddPolicy("MemberPolicy", policy => policy.RequireRole("Member"));

                opt.AddPolicy("AdminOrManagerPolicy", policy => policy.RequireRole("Admin", "Manager"));
                opt.AddPolicy("AdminAndManagerPolicy", policy => policy.RequireRole("Admin").RequireRole("Manager"));
                opt.AddPolicy("AllRolePolicy", policy => policy.RequireRole("Admin", "Manager", "Member"));

                opt.AddPolicy("AdminEmailPolicy", policy => policy.RequireClaim(ClaimTypes.Email, "admin@automarket.com"));
                opt.AddPolicy("AdminSurnamePolicy", policy => policy.RequireClaim(ClaimTypes.Surname, "admin"));
                opt.AddPolicy("ManagerEmailAndManagerSurnamePolicy", policy => policy.RequireClaim(ClaimTypes.Surname, "manager")
                    .RequireClaim(ClaimTypes.Email, "manager@automarket.com"));
            });

            services.AddSingleton(TimeProvider.System);

            return services;
        }
    }
}
