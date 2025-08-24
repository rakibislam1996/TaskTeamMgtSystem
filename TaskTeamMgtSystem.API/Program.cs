using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using TaskTeamMgtSystem.Infrastructure;
using TaskTeamMgtSystem.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using FluentValidation;
using MediatR;
using TaskTeamMgtSystem.Application.Common.Behaviors;
using TaskTeamMgtSystem.API.Middleware;
using Serilog;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// -------------------- SERVICES --------------------

// Add controllers
builder.Services.AddControllers();

// Swagger / API Explorer with JWT Support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Task Team Management System API",
        Version = "v1"
    });

    // 🔑 JWT Authorization in Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' followed by a space and then your token.\n\nExample: Bearer eyJhbGciOi..."
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// Register DbContext with SQL Server provider
builder.Services.AddDbContext<TaskTeamMgtSystemDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<TaskTeamMgtSystem.Application.Users.Validators.CreateUserCommandValidator>();

// Register MediatR and pipeline behaviors
builder.Services.AddMediatR(typeof(TaskTeamMgtSystem.Application.Users.Commands.CreateUserCommand).Assembly);
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

// Register application services
builder.Services.AddScoped<TaskTeamMgtSystem.Application.Common.Services.IAuthorizationService, TaskTeamMgtSystem.Application.Common.Services.AuthorizationService>();

// JWT Authentication configuration
var jwtSettings = builder.Configuration.GetSection("JWT");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"] ?? string.Empty);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key),
        RoleClaimType = ClaimTypes.Role // <-- Important: map role claim
    };
    
    // Add better error handling
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine($"Authentication failed: {context.Exception.Message}");
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            Console.WriteLine($"Token validated for user: {context.Principal?.Identity?.Name}");
            return Task.CompletedTask;
        }
    };
});

// Authorization policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("Manager", policy => policy.RequireRole("Manager"));
    options.AddPolicy("Employee", policy => policy.RequireRole("Employee"));
});

// Serilog logging
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();

var app = builder.Build();

// -------------------- MIDDLEWARE --------------------

// Global exception handling
app.UseMiddleware<ErrorHandlingMiddleware>();

// Swagger for development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Task Team Management System API v1");
    });
}

// HTTPS, Authentication, Authorization
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// -------------------- DATABASE --------------------
// Apply migrations and seed default users
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TaskTeamMgtSystemDbContext>();

    // Apply pending migrations (creates tables if missing)
    db.Database.Migrate();

    // Seed default users if they don't exist
    await DbSeeder.SeedAsync(db);
}

// -------------------- ROUTES --------------------

// Map controllers
app.MapControllers();

// Debug endpoint to test JWT token
app.MapGet("/debug/token", (ClaimsPrincipal user) =>
{
    return Results.Ok(new
    {
        IsAuthenticated = user.Identity?.IsAuthenticated,
        UserId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value,
        Email = user.FindFirst(ClaimTypes.Email)?.Value,
        Role = user.FindFirst(ClaimTypes.Role)?.Value,
        Claims = user.Claims.Select(c => new { c.Type, c.Value }).ToList()
    });
}).RequireAuthorization();

// Minimal API for login
app.MapPost("/login", async (TaskTeamMgtSystemDbContext db, IConfiguration config, LoginRequest login) =>
{
    var user = await db.Users.FirstOrDefaultAsync(u => u.Email == login.Email);
    if (user == null)
    {
        Log.Warning("Authentication failed for email: {Email}", login.Email);
        return Results.Unauthorized();
    }

    var passwordHasher = new PasswordHasher<User>();
    var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash ?? string.Empty, login.Password);
    if (result == PasswordVerificationResult.Failed)
    {
        Log.Warning("Authentication failed for email: {Email}", login.Email);
        return Results.Unauthorized();
    }

    var jwtSettings = config.GetSection("JWT");
    var key = Encoding.UTF8.GetBytes(jwtSettings["Key"] ?? string.Empty);
    var claims = new[]
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
        new Claim(ClaimTypes.Role, user.Role ?? string.Empty)
    };
    var token = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(
        issuer: jwtSettings["Issuer"],
        audience: jwtSettings["Audience"],
        claims: claims,
        expires: DateTime.UtcNow.AddHours(2),
        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
    );
    var tokenString = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler().WriteToken(token);
    return Results.Ok(new { token = tokenString, role = user.Role });
})
.WithName("Login");

app.Run();

// -------------------- RECORDS --------------------
public record LoginRequest(string Email, string Password);
