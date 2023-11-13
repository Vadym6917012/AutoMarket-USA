using AutoMarket.Server;
using AutoMarket.Server.Core;
using AutoMarket.Server.Core.Models;
using AutoMarket.Server.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var connectingString = builder.Configuration.GetConnectionString("AutoMarketConnection");

// Налаштування з`єднання до БД
builder.Services.AddDbContext<DataContext>(option =>
{
    option.UseSqlServer(connectingString);
});

// Налаштування IdentityCore юзера
builder.Services.AddIdentityCore<User>(option =>
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
    .AddSignInManager<SignInManager<User>>()
    .AddUserManager<UserManager<User>>()
    .AddDefaultTokenProviders();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(AppAutoMapper).Assembly);

// Конфігурація Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "AutoMarketAPI",
        Description = "Open API for AutoMarket web application",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Email = "vadym.radchuk@oa.edu.ua",
            Name = "Vadym Radchuk"
        }
    });
});

// Додавання Identity та EF
builder.Services.AddIdentityApiEndpoints<User>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<DataContext>();

// Конфігурація маршрутизації
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Додавання репозиторіїв з областями видимості "Scoped"
builder.Services.AddScoped<JWTServices>();
builder.Services.AddScoped<Repository<BodyType>>();
builder.Services.AddScoped<ModelRepository>();
builder.Services.AddScoped<GenerationRepository>();
builder.Services.AddScoped<MakeRepository>();
builder.Services.AddScoped<Repository<FuelType>>();
builder.Services.AddScoped<Repository<GearBoxType>>();
builder.Services.AddScoped<ModificationRepository>();
builder.Services.AddScoped<CarRepository>();
builder.Services.AddScoped<ImagesRepository>();
builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<ProducingCountryRepository>();
builder.Services.AddScoped<ContextSeedService>();
builder.Services.AddScoped<Repository<DriveTrain>>();
builder.Services.AddScoped<Repository<TechnicalCondition>>();


// Додавання контролерів з можливістю відображення та налаштування серіалізації JSON
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

// Додавання авторизації використовуючи JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(option =>
    {
        option.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddCors();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = actionContext =>
    {
        var errors = actionContext.ModelState
        .Where(x => x.Value.Errors.Count > 0)
        .SelectMany(x => x.Value.Errors)
        .Select(x => x.ErrorMessage).ToArray();

        var toReturn = new
        {
            Errors = errors
        };

        return new BadRequestObjectResult(toReturn);
    };
});

builder.Services.AddAuthorization(opt =>
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
    opt.AddPolicy("VIPPolicy", policy => policy.RequireAssertion(context => SD.VIPPolicy(context)));
});

var app = builder.Build();

// Конфігурація конвеєра обробки HTTP-запитів
app.UseCors(opt =>
{
    opt.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins(builder.Configuration["JWT:ClientUrl"]);
});
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHsts();
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(app.Environment.WebRootPath, "User Photos")),
    RequestPath = "/User Photos"
});

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(app.Environment.WebRootPath, "MakeIcons")),
    RequestPath = "/MakeIcons"
});

app.MapIdentityApi<User>();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

#region ContextSeed

using var scope = app.Services.CreateScope();

try
{
    var contextSeedService = scope.ServiceProvider.GetService<ContextSeedService>();
    await contextSeedService.InitializeContextAsync();
}
catch (Exception ex)
{

    var logger = scope.ServiceProvider.GetService<ILogger<Program>>();
    logger.LogError(ex.Message, "Не вдалося ініціалізувати та заповнити базу даних");
}

#endregion

app.Run();
