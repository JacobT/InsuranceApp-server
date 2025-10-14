using InsuranceApp.Api.Infrastructure;
using InsuranceApp.Api.Managers.Interfaces;
using InsuranceApp.Api.Models;
using InsuranceApp.Api.Services.Interfaces;
using InsuranceApp.Data.Models;
using Microsoft.AspNetCore.Identity;


namespace InsuranceApp.Api.Managers;

/// <summary>
/// Implements <see cref="IAuthManager"/> for user authentication and registration using ASP.NET Identity.
/// </summary>
public class AuthManager : IAuthManager
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ITokenService _tokenService;

    public AuthManager(UserManager<IdentityUser> userManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }

    /// <inheritdoc />
    /// <remarks>
    /// Creates a new user with a default role of <see cref="UserRoles.User"/>.
    /// </remarks>
    public async Task<IdentityResult> RegisterAsync(RegisterDTO registerDto)
    {
        IdentityUser user = new IdentityUser
        {
            UserName = registerDto.Email.ToLowerInvariant(),
            Email = registerDto.Email.ToLowerInvariant()
        };

        IdentityResult result = await _userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded)
        {
            return result;
        }

        //default user role, TODO user validation by admin/manager
        await _userManager.AddToRoleAsync(user, UserRoles.User);
        return result;
    }

    /// <inheritdoc />
    /// <remarks>
    /// Generates a JWT token containing the user's ID, email, and roles if authentication succeeds.
    /// Returns <c>null</c> if authentication fails.
    /// </remarks>
    public async Task<JwtTokenDTO?> LoginAsync(LoginDTO loginDto)
    {
        IdentityUser? user = await _userManager.FindByEmailAsync(loginDto.Email.ToLowerInvariant());
        if (user is null)
        {
            return null;
        }

        bool result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
        if (!result)
        {
            return null;
        }

        string jwtToken = await _tokenService.GenerateJwtTokenAsync(user);
        RefreshTokenEntity refreshTokenEntity = await _tokenService.IssueRefreshTokenAsync(user);

        return new JwtTokenDTO
        {
            JwtToken = jwtToken,
            RefreshToken = refreshTokenEntity.RefreshToken!
        };
    }

    /// <inheritdoc />
    /// <remarks>
    /// This method checks the validity of the refresh token, generates a new JWT token, 
    /// and updates the refresh token in the database.
    /// </remarks>
    public async Task<JwtTokenDTO?> RefreshTokenAsync(RefreshTokenDTO refreshTokenDto)
    {
        RefreshTokenEntity? refreshedTokenEntity = await _tokenService.IssueRefreshTokenAsync(refreshTokenDto.RefreshToken);
        if (refreshedTokenEntity is null)
        {
            return null;
        }

        return new JwtTokenDTO
        {
            JwtToken = await _tokenService.GenerateJwtTokenAsync(refreshedTokenEntity.User),
            RefreshToken = refreshedTokenEntity.RefreshToken!
        };
    }

    /// <inheritdoc />
    /// <remarks>
    /// This method supports "logout" functionality by removing the refresh token,
    /// preventing further token renewal for the user until they log in again.
    /// </remarks>
    public async Task<bool> LogoutAsync(string userId)
    {
        RefreshTokenEntity? deletedRefreshTokenEntity = await _tokenService.DeleteRefreshTokenAsync(userId);
        return deletedRefreshTokenEntity is not null;
    }
}
