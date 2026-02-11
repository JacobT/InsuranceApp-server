namespace InsuranceApp.Api.Models.Interfaces;

/// <summary>
/// Defines a contract for all Data Transfer Objects (DTOs) in the application.
/// </summary>
public interface IDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the DTO.
    /// </summary>
    int Id { get; set; }
}
