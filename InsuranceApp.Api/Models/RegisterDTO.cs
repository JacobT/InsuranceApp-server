using System.ComponentModel.DataAnnotations;

namespace InsuranceApp.Api.Models;

/// <summary>
/// Represents the data required to register a new user.
/// </summary>
public class RegisterDTO
{
    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    public required string Password { get; set; }
}
