using InsuranceApp.Api.Infrastructure;
using InsuranceApp.Api.Infrastructure.Extensions;
using InsuranceApp.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Get the connection string from configuration
var connectionString = builder.Configuration.GetConnectionString("LocalInsuranceConnection");

// Add the DbContext with SQL Server provider
builder.Services.AddDbContext<InsuranceDbContext>(cfg =>
    cfg.UseSqlServer(connectionString));

// Configure Identity, JWT authentication, and authorization policies
builder.Services.AddAppIdentity(builder.Configuration);

// Register repositories, managers, and services for DI
builder.Services.AddRepositories()
                .AddManagers()
                .AddServices();

// Add controller support
builder.Services.AddControllers()
    .AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

// Configure AutoMapper with mapping profiles
builder.Services.AddAutoMapper(cfg => { cfg.AddProfile<MapperProfile>(); });

// Configure Swagger for API documentation
builder.Services.AddAppSwagger();

// Creates cors policy for development purposes
builder.Services.CreateCorsDevPolicy();

var app = builder.Build();

// Enable Swagger UI in development
app.UseAppSwagger();

// Adds cors policy for development
app.AddCorsDevPolicy();

// Redirect HTTP requests to HTTPS
app.UseHttpsRedirection();

// Add authentication and authorization middleware
app.UseAuthentication()
   .UseAuthorization();

// Global exception handling middleware
app.UseGlobalExeptionHandler();

// Map controller routes
app.MapControllers();

// Seed initial data (roles, default admin)
await DataSeeder.SeedDataAsync(app);

// Run the application
app.Run();
