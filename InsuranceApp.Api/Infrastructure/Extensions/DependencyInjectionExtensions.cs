using InsuranceApp.Api.Managers;
using InsuranceApp.Api.Managers.Interfaces;
using InsuranceApp.Api.Services;
using InsuranceApp.Api.Services.Interfaces;
using InsuranceApp.Data.Repositories;
using InsuranceApp.Data.Repositories.Interfaces;

namespace InsuranceApp.Api.Infrastructure.Extensions;

/// <summary>
/// Provides extension methods for registering application services, managers, and repositories 
/// into the dependency injection container.
/// </summary>
public static class DependencyInjectionExtensions
{
    /// <summary>
    /// Registers all application managers with scoped lifetimes.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> used to configure DI.</param>
    /// <returns>The updated <see cref="IServiceCollection"/> for method chaining.</returns>
    /// <remarks>
    /// Managers encapsulate business logic and orchestrate operations between repositories and services.
    /// </remarks>
    public static IServiceCollection AddManagers(this IServiceCollection services)
    {
        return services.AddScoped<ICustomerManager, CustomerManager>()
                       .AddScoped<IInsuranceManager, InsuranceManager>()
                       .AddScoped<IInsuranceClaimManager, InsuranceClaimManager>()
                       .AddScoped<IAuthManager, AuthManager>();
    }

    /// <summary>
    /// Registers generic services with scoped lifetimes.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> used to configure DI.</param>
    /// <returns>The updated <see cref="IServiceCollection"/> for method chaining.</returns>
    /// <remarks>
    /// Services provide reusable application logic that can be consumed by managers.
    /// </remarks>
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services.AddScoped(typeof(IDetailService<,>), typeof(DetailService<,>))
                       .AddScoped(typeof(IRelatedEntityService<,,,>), typeof(RelatedEntityService<,,,>))
                       .AddScoped(typeof(IFilterService<,>), typeof(FilterService<,>))
                       .AddScoped<ITokenService, TokenService>();
    }

    /// <summary>
    /// Registers all repositories with scoped lifetimes.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> used to configure DI.</param>
    /// <returns>The updated <see cref="IServiceCollection"/> for method chaining.</returns>
    /// <remarks>
    /// Repositories provide data access using Entity Framework Core and encapsulate persistence logic.
    /// </remarks>
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services.AddScoped<ICustomerRepository, CustomerRepository>()
                       .AddScoped<IInsuranceRepository, InsuranceRepository>()
                       .AddScoped<IInsuranceClaimRepository, InsuranceClaimRepository>()
                       .AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
    }
}
