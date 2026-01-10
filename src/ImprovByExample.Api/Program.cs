using FluentValidation;
using ImprovByExample.Application.Common.Interfaces.Repositories;
using ImprovByExample.Application.Common.Interfaces.Services;
using ImprovByExample.Application.Services;
using ImprovByExample.Infrastructure.Data;
using ImprovByExample.Infrastructure.Repositories;
using ImprovByExample.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add database context
builder.Services.AddDbContext<ImprovDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL")));

// Add Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // Password requirements
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 8;
    
    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
    options.Lockout.MaxFailedAccessAttempts = 5;
    
    // User settings
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<ImprovDbContext>()
.AddDefaultTokenProviders();

// Add authentication and authorization
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

// Add repositories
builder.Services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Add services
builder.Services.AddScoped<IActivityService, ActivityService>();

// Add FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<IActivityService>();

// Add HttpContextAccessor for accessing current user
builder.Services.AddHttpContextAccessor();

// Add controllers with JSON options
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
    });

// Add OpenAPI services
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
