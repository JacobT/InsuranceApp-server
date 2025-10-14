using InsuranceApp.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace InsuranceApp.Api.Services.Interfaces;

/// <summary>
/// Defines functionality for generating security tokens used in authentication.
/// </summary>
public interface ITokenService
{
    /// <summary>
    /// Generates a signed JWT for a specified user, embedding user claims and roles.
    /// </summary>
    /// <param name="user">The user for whom the JWT will be generated.</param>
    /// <returns>A signed JWT as a string.</returns>
    Task<string> GenerateJwtTokenAsync(IdentityUser user);

    /// <summary>
    /// Issues a refresh token for the specified user.
    /// Creates a new token if none exists, or updates the existing token.
    /// </summary>
    /// <param name="user">The user for whom the refresh token should be issued.</param>
    /// <returns>The created or updated <see cref="RefreshTokenEntity"/>.</returns>
    Task<RefreshTokenEntity> IssueRefreshTokenAsync(IdentityUser user);

    /// <summary>
    /// Validates an existing refresh token and issues a new one if valid.
    /// Deletes expired tokens.
    /// </summary>
    /// <param name="refreshToken">The refresh token string to validate.</param>
    /// <returns>
    /// The newly created or updated <see cref="RefreshTokenEntity"/> if the token is valid; 
    /// otherwise, <c>null</c> if the token is invalid or expired.
    /// </returns>
    Task<RefreshTokenEntity?> IssueRefreshTokenAsync(string refreshToken);

    /// <summary>
    /// Deletes the refresh token associated with the specified user, typically used during logout.
    /// </summary>
    /// <param name="user">The user whose refresh token should be deleted.</param>
    /// <returns>
    /// The deleted <see cref="RefreshTokenEntity"/> if a token existed; otherwise, <c>null</c> if no token was found for the user.
    /// </returns>
    Task<RefreshTokenEntity?> DeleteRefreshTokenAsync(string userId);
}
