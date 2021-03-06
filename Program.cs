using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using HotDeskAPI;
using HotDeskAPI.Authorization;
using HotDeskAPI.Entities;
using HotDeskAPI.Middleware;
using HotDeskAPI.Models;
using HotDeskAPI.Models.Validators;
using HotDeskAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

[assembly: InternalsVisibleTo("HotDeskAPI.IntegrationTests")]

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var authenticationSettings = new AuthenticationSettings();
builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);
builder.Services.AddSingleton(authenticationSettings);

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";
    option.DefaultScheme = "Bearer";
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = authenticationSettings.JwtIssuer,
        ValidAudience = authenticationSettings.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),
    };
});
builder.Services.AddScoped<IAuthorizationHandler, ResourceOperationRequirementHandler>();
builder.Services.AddControllers().AddFluentValidation();
builder.Services.AddDbContext<HotDeskDbContext>
    (options => options.UseSqlServer(builder.Configuration.GetConnectionString("HotDeskDbConnection")));
builder.Services.AddScoped<Seeder>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IDeskService, DeskService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();
builder.Services.AddScoped<IValidator<AddDeskDto>, AddDeskDtoValidator>();
builder.Services.AddScoped<IValidator<AddLocationDto>, AddLocationDtoValidator>();
builder.Services.AddScoped<IValidator<AddReservationDto>, AddReservationDtoValidator>();
builder.Services.AddScoped<IValidator<ChangeDeskDto>, ChangeDeskDtoValidator>();
builder.Services.AddScoped<IUserContextService, UserContextService>();
builder.Services.AddScoped<ErrorHandlingMiddleware>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<Seeder>();
seeder.Seed();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseAuthentication();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
public partial class Startup{}