using InsuranceApp.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace InsuranceApp.Api.Infrastructure.Extensions;

/// <summary>
/// Provides extension methods for configuring ASP.NET Core Identity and JWT authentication.
/// </summary>
public static class IdentityExtensions
{
    /// <summary>
    /// Registers ASP.NET Core Identity, JWT authentication, and authorization policies.
    /// </summary>
    /// <param name="services">The application's service collection.</param>
    /// <param name="config">The application configuration (used for JWT settings).</param>
    /// <returns>The updated <see cref="IServiceCollection"/> for chaining.</returns>
    /// <remarks>
    /// <para>
    /// This method:
    /// <list type="bullet">
    /// <item><description>Configures Identity with relaxed password requirements (e.g., no non-alphanumeric or uppercase required).</description></item>
    /// <item><description>Adds role support (<see cref="IdentityRole"/>) and token providers.</description></item>
    /// <item><description>Registers <see cref="InsuranceDbContext"/> as the EF Core store for Identity.</description></item>
    /// <item><description>Configures JWT bearer authentication using settings from the <c>Jwt</c> section in <c>appsettings.json</c>.</description></item>
    /// <item><description>Adds authentication policies.</description></item>
    /// </list>
    /// </para>
    /// </remarks>
    public static IServiceCollection AddAppIdentity(this IServiceCollection services, IConfiguration config)
    {
        // Configure ASP.NET Core Identity
        services.AddIdentityCore<IdentityUser>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 6;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;

            options.User.RequireUniqueEmail = true;
        })
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<InsuranceDbContext>()
        .AddDefaultTokenProviders();

        // Configure JWT authentication
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            IConfigurationSection jwt = config.GetSection("Jwt");
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwt["Issuer"],

                ValidateAudience = true,
                ValidAudience = jwt["Audience"],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]!)),

                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromMinutes(1)
            };
        });

        // Configure authorization policies
        services.AddAuthorization(options =>
        {
            options.AddPolicy(Policies.AdminOrManager, policy => policy.RequireRole(UserRoles.Admin, UserRoles.Manager));
        });

        return services;
    }
}
