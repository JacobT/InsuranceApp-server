using System.ComponentModel.DataAnnotations;

namespace InsuranceApp.Api.Models;

/// <summary>
/// Represents the login refresh token.
/// </summary>
public class RefreshTokenDTO
{
    [Required]
    public required string RefreshToken { get; set; }
}
