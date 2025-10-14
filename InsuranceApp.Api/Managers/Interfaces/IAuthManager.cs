using InsuranceApp.Api.Models;
using Microsoft.AspNetCore.Identity;

namespace InsuranceApp.Api.Managers.Interfaces;

/// <summary>
/// Provides authentication operations, including user registration and login.
/// </summary>
public interface IAuthManager
{
    /// <summary>
    /// Authenticates a user and generates a JWT token if successful.
    /// </summary>
    /// <param name="loginDto">The user's login credentials.</param>
    /// <returns>
    /// A <see cref="JwtTokenDTO"/> containing a new JWT token and refresh token if authentication succeeds; otherwise, <c>null</c>.
    /// </returns>
    Task<JwtTokenDTO?> LoginAsync(LoginDTO loginDto);

    /// <summary>
    /// Registers a new user with the specified credentials.
    /// </summary>
    /// <param name="registerDto">The user's registration information.</param>
    /// <returns>
    /// An <see cref="IdentityResult"/> indicating success or failure of the registration.
    /// </returns>
    Task<IdentityResult> RegisterAsync(RegisterDTO registerDto);

    /// <summary>
    /// Validates the provided refresh token and issues a new JWT token if valid.
    /// </summary>
    /// <param name="refreshTokenDto">The refresh token provided by the client.</param>
    /// <returns>
    /// A <see cref="JwtTokenDTO"/> containing a new JWT token and refresh token if the provided token is valid;
    /// otherwise, <c>null</c> if the token is invalid or expired.
    /// </returns>
    Task<JwtTokenDTO?> RefreshTokenAsync(RefreshTokenDTO refreshTokenDto);

    /// <summary>
    /// Logs out a user by deleting their refresh token from the database.
    /// </summary>
    /// <param name="userId">The ID of the authenticated user whose refresh token should be deleted.</param>
    /// <returns>
    /// <c>true</c> if a refresh token was found and successfully deleted;
    /// otherwise, <c>false</c> if no refresh token existed for the user.
    /// </returns>
    Task<bool> LogoutAsync(string userId);
}
