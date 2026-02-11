using InsuranceApp.Data.Models;

namespace InsuranceApp.Data.Repositories.Interfaces;

/// <summary>
/// Defines the contract for refresh token repository operations.
/// Provides methods to retrieve, create, update, and delete refresh tokens.
/// </summary>
public interface IRefreshTokenRepository
{
    /// <summary>
    /// Retrieves a refresh token entity associated with the specified user ID.
    /// </summary>
    /// <param name="userId">The ID of the user whose refresh token is being retrieved.</param>
    /// <returns>
    /// The matching <see cref="RefreshTokenEntity"/> if found; otherwise <c>null</c>.
    /// </returns>
    Task<RefreshTokenEntity?> GetByUserIdAsync(string userId);

    /// <summary>
    /// Retrieves a refresh token entity by its token string.
    /// </summary>
    /// <param name="tokenHash">The token string to look up.</param>
    /// <returns>
    /// The matching <see cref="RefreshTokenEntity"/> if found; otherwise <c>null</c>.
    /// </returns>
    Task<RefreshTokenEntity?> GetByTokenHashAsync(string tokenHash);

    /// <summary>
    /// Inserts a new refresh token entity into the database.
    /// </summary>
    /// <param name="refreshToken">The refresh token to insert.</param>
    /// <returns>The inserted <see cref="RefreshTokenEntity"/>.</returns>
    Task<RefreshTokenEntity> InsertAsync(RefreshTokenEntity refreshToken);

    /// <summary>
    /// Updates an existing refresh token entity with new values.
    /// </summary>
    /// <param name="refreshToken">The existing refresh token entity.</param>
    /// <param name="modifiedRefreshToken">The modified refresh token values.</param>
    /// <returns>The updated <see cref="RefreshTokenEntity"/>.</returns>
    RefreshTokenEntity Update(RefreshTokenEntity refreshToken, RefreshTokenEntity modifiedRefreshToken);

    /// <summary>
    /// Deletes a refresh token entity from the database.
    /// </summary>
    /// <param name="refreshToken">The refresh token entity to delete.</param>
    void Delete(RefreshTokenEntity refreshToken);

    /// <summary>
    /// Persists any changes made in the repository to the database.
    /// </summary>
    Task SaveChangesAsync();
}