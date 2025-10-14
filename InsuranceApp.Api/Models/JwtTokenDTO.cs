using System.ComponentModel.DataAnnotations;

namespace InsuranceApp.Api.Models;

/// <summary>
/// Represents a pair of tokens used for authentication:
/// a short-lived JWT for API access and a longer-lived refresh token
/// for renewing the JWT when it expires.
/// </summary>
public class JwtTokenDTO
{
    [Required]
    public required string JwtToken { get; set; }

    [Required]
    public required string RefreshToken { get; set; }
}
