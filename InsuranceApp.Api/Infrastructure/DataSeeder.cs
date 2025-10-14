using Microsoft.AspNetCore.Identity;

namespace InsuranceApp.Api.Infrastructure;

/// <summary>
/// Provides functionality to seed initial application data,
/// including default user roles and a default administrator account.
/// </summary>
public static class DataSeeder
{
    /// <summary>
    /// Seeds roles and a default administrator account into the database.
    /// </summary>
    /// <param name="app">The <see cref="WebApplication"/> used to access services and configuration.</param>
    /// <remarks>
    /// This method:
    /// <list type="bullet">
    /// <item><description>Ensures that all roles defined in <see cref="UserRoles"/> exist.</description></item>
    /// <item><description>Creates a default administrator user using credentials from <c>appsettings.json</c> under <c>DefaultAdmin</c>.</description></item>
    /// <item><description>Adds the admin user to the <c>admin</c> role.</description></item>
    /// </list>
    /// </remarks>
    public static async Task SeedDataAsync(WebApplication app)
    {
        using IServiceScope scope = app.Services.CreateScope();
        RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        UserManager<IdentityUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

        //seed user roles
        foreach (string role in UserRoles.AllRoles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        //seed default admin
        IConfigurationSection admin = app.Configuration.GetSection("DefaultAdmin");
        string adminEmail = admin["Email"]!;
        string adminPassword = admin["Password"]!;

        IdentityUser? adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (adminUser is null)
        {
            adminUser = new IdentityUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true
            };

            IdentityResult result = await userManager.CreateAsync(adminUser, adminPassword);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, UserRoles.Admin);
            }
        }
    }
}
