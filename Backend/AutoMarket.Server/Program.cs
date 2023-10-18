using AutoMapper;
using AutoMarket.Server.Core;
using AutoMarket.Server.Infrastructure;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
var connectingString = builder.Configuration.GetConnectionString("AutoMarketConnection");
// Add services to the container.

builder.Services.AddDbContext<DataContext>(option =>
{
    option.UseSqlServer(connectingString);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAutoMapper(typeof(AppAutoMapper).Assembly);

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

builder.Services.AddAuthorization();

builder.Services.AddIdentityApiEndpoints<User>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<DataContext>();

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddScoped<Repository<BodyType>>();
builder.Services.AddScoped<ModelRepository>();
builder.Services.AddScoped<GenerationRepository>();
builder.Services.AddScoped<Repository<Make>>();
builder.Services.AddScoped<Repository<ProducingCountry>>();
builder.Services.AddScoped<Repository<FuelType>>();
builder.Services.AddScoped<Repository<GearBoxType>>();
builder.Services.AddScoped<Repository<Modification>>();
builder.Services.AddScoped<CarRepository>();
builder.Services.AddScoped<ImagesRepository>();

builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapIdentityApi<User>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
