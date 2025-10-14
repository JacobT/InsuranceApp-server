using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceApp.Api.Infrastructure.Extensions;

/// <summary>
/// Provides middleware extensions for centralized exception handling.
/// </summary>
public static class ExceptionMiddlewareExtensions
{
    /// <summary>
    /// Configures a global exception handler that intercepts unhandled exceptions
    /// and returns a standardized <see cref="ProblemDetails"/> response.
    /// </summary>
    /// <param name="app">The <see cref="WebApplication"/> instance.</param>
    /// <returns>The <see cref="WebApplication"/> for method chaining.</returns>
    /// <remarks>
    /// <para>
    /// This middleware:
    /// <list type="bullet">
    /// <item><description>Logs unhandled exceptions to the console (placeholder for a real logger).</description></item>
    /// <item><description>Returns an RFC 7807 <see cref="ProblemDetails"/> JSON response with HTTP 500 status code.</description></item>
    /// <item><description>In <c>Development</c> environment, includes full exception details in the response <c>Detail</c> field; otherwise hides them.</description></item>
    /// <item><description>Ensures consistent error responses across the API.</description></item>
    /// </list>
    /// </para>
    /// </remarks>
    public static WebApplication UseGlobalExeptionHandler(this WebApplication app)
    {
        app.UseExceptionHandler(error =>
        {
            error.Run(async context =>
            {
                var exceptionFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                if (exceptionFeature is not null)
                {
                    //TODO Replace Console.WriteLine with a real logging framework
                    Console.WriteLine(exceptionFeature.Error.ToString(), $"Unhandled exception for request {exceptionFeature.Path}");

                    var errorResponse = new ProblemDetails
                    {
                        Status = StatusCodes.Status500InternalServerError,
                        Title = "An unexpected error occurred.",
                        Detail = app.Environment.IsDevelopment() ? exceptionFeature.Error.ToString() : null
                    };

                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/problem+json";
                    await context.Response.WriteAsJsonAsync(errorResponse);
                }
            });
        });
        return app;
    }

}
