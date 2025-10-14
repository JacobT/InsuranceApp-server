using InsuranceApp.Api.Services.Interfaces;
using InsuranceApp.Data.Models;
using InsuranceApp.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace InsuranceApp.Api.Services;

/// <summary>
/// Implements <see cref="ITokenService"/> to generate JWT and refresh tokens.
/// </summary>
public class TokenService : ITokenService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IConfiguration _config;
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public TokenService(UserManager<IdentityUser> userManager, IConfiguration config, IRefreshTokenRepository refreshTokenRepository)
    {
        _userManager = userManager;
        _config = config;
        _refreshTokenRepository = refreshTokenRepository;
    }

    /// <inheritdoc />
    public async Task<string> GenerateJwtTokenAsync(IdentityUser user)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        // Add user roles as claims
        IList<string>? roles = await _userManager.GetRolesAsync(user);
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        // Create signing key and credentials
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // Build token
        JwtSecurityToken token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["Jwt:ExpiresMinutes"])),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    /// <summary>
    /// Generates a cryptographically secure refresh token.
    /// </summary>
    /// <returns>A Base64-encoded random string representing the refresh token.</returns>
    private static string GenerateRefreshToken()
    {
        byte[] randomNumber = RandomNumberGenerator.GetBytes(32);
        return Convert.ToBase64String(randomNumber);
    }

    /// <summary>
    /// Determines whether a <see cref="DbUpdateException"/> is caused by a unique constraint violation in the database.
    /// </summary>
    /// <param name="ex">The exception thrown during a database update.</param>
    /// <returns>
    /// <c>true</c> if the exception corresponds to a SQL unique constraint violation (2601 or 2627); otherwise <c>false</c>.
    /// </returns>
    /// <remarks>
    /// SQL Server error 2601 occurs when a duplicate key is inserted into a unique index, 
    /// while 2627 occurs for primary key or unique constraint violations. 
    /// This method allows retrying token generation in case of such rare collisions.
    /// </remarks>
    private static bool IsUniqueConstraintViolation(DbUpdateException ex)
    {
        if (ex.InnerException is SqlException sqlEx)
        {
            return sqlEx.Number == 2627 || sqlEx.Number == 2601;
        }

        return false;
    }

    private static string HashToken(string token)
    {
        byte[] hashBytes = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(token));
        return Convert.ToBase64String(hashBytes);
    }

    /// <summary>
    /// Creates a new refresh token or updates an existing one for the specified user.
    /// Retries token generation if a unique constraint violation occurs to ensure token uniqueness.
    /// </summary>
    /// <param name="user">The user for whom the refresh token is being issued.</param>
    /// <param name="currentRefreshTokenEntity">The current refresh token entity for the user, if any.</param>
    /// <returns>The newly created or updated <see cref="RefreshTokenEntity"/>.</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if a unique refresh token cannot be issued after a defined number of retry attempts.
    /// </exception>
    /// <remarks>
    /// Tokens are generated using cryptographically secure randomness. 
    /// Because collisions are theoretically possible, this method will retry up to <c>maxRetries</c> times 
    /// if a unique constraint violation occurs during database insert or update.
    /// </remarks>
    private async Task<RefreshTokenEntity> CreateOrUpdateTokenAsync(IdentityUser user, RefreshTokenEntity? currentRefreshTokenEntity)
    {
        int retries = 0;
        const int maxRetries = 3;
        do
        {
            try
            {
                string refreshToken = GenerateRefreshToken();
                RefreshTokenEntity newRefreshTokenEntity = new RefreshTokenEntity
                {
                    Id = currentRefreshTokenEntity?.Id ?? 0,
                    UserId = user.Id,
                    RefreshTokenHash = HashToken(refreshToken),
                    RefreshToken = refreshToken,
                    ExpirationDate = DateTime.UtcNow.AddDays(Convert.ToDouble(_config["Jwt:RefreshExpiresDays"])),
                    User = user
                };

                if (currentRefreshTokenEntity is null)
                {
                    await _refreshTokenRepository.InsertAsync(newRefreshTokenEntity);
                }
                else
                {
                    _refreshTokenRepository.Update(currentRefreshTokenEntity, newRefreshTokenEntity);
                }

                await _refreshTokenRepository.SaveChangesAsync();
                return newRefreshTokenEntity;
            }
            catch (DbUpdateException ex) when (IsUniqueConstraintViolation(ex))
            {
                //token already exists -> retry
                retries++;
                continue;
            }
        } while (retries < maxRetries);

        throw new InvalidOperationException("Unable to issue unique refresh token after multiple attempts.");
    }

    /// <inheritdoc />
    /// <remarks>
    /// If the user already has a refresh token, it will be updated; otherwise, a new one is created.
    /// This method ensures that only one active refresh token exists per user.
    /// </remarks>
    public async Task<RefreshTokenEntity> IssueRefreshTokenAsync(IdentityUser user)
    {
        RefreshTokenEntity? currentRefreshTokenEntity = await _refreshTokenRepository.GetByUserIdAsync(user.Id);

        return await CreateOrUpdateTokenAsync(user, currentRefreshTokenEntity);
    }

    /// <inheritdoc />
    /// <remarks>
    /// Expired tokens are deleted to keep the database clean. 
    /// This method ensures that each refresh token can be used only once to issue a new JWT.
    /// </remarks>
    public async Task<RefreshTokenEntity?> IssueRefreshTokenAsync(string refreshToken)
    {
        string refreshTokenHash = HashToken(refreshToken);
        RefreshTokenEntity? currentRefreshTokenEntity = await _refreshTokenRepository.GetByTokenHashAsync(refreshTokenHash);

        if (currentRefreshTokenEntity is null || currentRefreshTokenEntity.ExpirationDate < DateTime.UtcNow)
        {
            if (currentRefreshTokenEntity is not null)
            {
                _refreshTokenRepository.Delete(currentRefreshTokenEntity);
            }

            return null;
        }

        return await CreateOrUpdateTokenAsync(currentRefreshTokenEntity.User, currentRefreshTokenEntity);
    }

    /// <inheritdoc />
    /// <remarks>
    /// This method ensures that once a user logs out, their refresh token is removed from the database, 
    /// preventing further use for obtaining new JWTs. 
    /// If no token exists for the user, the method returns <c>null</c>.
    /// </remarks>
    public async Task<RefreshTokenEntity?> DeleteRefreshTokenAsync(string userId)
    {
        RefreshTokenEntity? refreshTokenEntity = await _refreshTokenRepository.GetByUserIdAsync(userId);
        if (refreshTokenEntity is null)
        {
            return null;
        }

        _refreshTokenRepository.Delete(refreshTokenEntity);
        await _refreshTokenRepository.SaveChangesAsync();

        return refreshTokenEntity;
    }
}
