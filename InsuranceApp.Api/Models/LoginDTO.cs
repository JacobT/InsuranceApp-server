using System.ComponentModel.DataAnnotations;

namespace InsuranceApp.Api.Models;

/// <summary>
/// Represents the data required to log in a user.
/// </summary>
public class LoginDTO
{
    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    public required string Password { get; set; }
}
