using Microsoft.OpenApi.Models;

namespace InsuranceApp.Api.Infrastructure.Extensions;

/// <summary>
/// Provides extension methods for configuring and enabling Swagger in the application.
/// </summary>
public static class SwaggerExtensions
{
    /// <summary>
    /// Adds Swagger and API explorer services to the DI container.
    /// </summary>
    /// <param name="services">The application's service collection.</param>
    /// <returns>The updated <see cref="IServiceCollection"/> for chaining.</returns>
    /// <remarks>
    /// This method:
    /// <list type="bullet">
    /// <item><description>Registers the API explorer to expose endpoint metadata.</description></item>
    /// <item><description>Configures Swagger with a <c>v1</c> document.</description></item>
    /// <item><description>Enables JWT Bearer authentication in Swagger UI.</description></item>
    /// </list>
    /// </remarks>
    public static IServiceCollection AddAppSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new() { Title = "InsuranceApp", Version = "v1" });

            // Configure JWT authentication in Swagger UI
            c.AddSecurityDefinition("Bearer", new()
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT"
            });

            c.AddSecurityRequirement(new()
            {
                {
                    new OpenApiSecurityScheme {
                        Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme, Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
        return services;
    }

    /// <summary>
    /// Enables the Swagger middleware and Swagger UI for the application.
    /// </summary>
    /// <param name="app">The <see cref="WebApplication"/> instance.</param>
    /// <returns>The updated <see cref="WebApplication"/> for chaining.</returns>
    /// <remarks>
    /// Swagger is only enabled in the <c>Development</c> environment.
    /// The Swagger UI is served at the application root (<c>/</c>).
    /// </remarks>
    public static WebApplication UseAppSwagger(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("swagger/v1/swagger.json", "InsuranceApp V1");
                c.RoutePrefix = string.Empty;
            });
        }
        return app;
    }
}
