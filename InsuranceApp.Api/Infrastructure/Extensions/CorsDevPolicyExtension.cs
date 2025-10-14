namespace InsuranceApp.Api.Infrastructure.Extensions;

public static class CorsDevPolicyExtension
{
    public static IServiceCollection CreateCorsDevPolicy(this IServiceCollection services)
    {
        return services.AddCors(options =>
        {
            options.AddPolicy("DevCorsPolicy", policy =>
            {
                policy.WithOrigins("http://localhost:5173")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });
    }

    public static WebApplication AddCorsDevPolicy(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseCors("DevCorsPolicy");
        }

        return app;
    }
}
