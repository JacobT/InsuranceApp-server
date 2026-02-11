using InsuranceApp.Data.Models;
using InsuranceApp.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InsuranceApp.Data.Repositories;

/// <summary>
/// Repository implementation for managing <see cref="RefreshTokenEntity"/> entities.
/// Provides methods to query and manipulate refresh tokens.
/// </summary>
public class RefreshTokenRepository : BaseRepository<RefreshTokenEntity>, IRefreshTokenRepository
{
    public RefreshTokenRepository(InsuranceDbContext context) : base(context)
    {
    }

    /// <inheritdoc />
    public async Task<RefreshTokenEntity?> GetByTokenHashAsync(string tokenHash) => await _dbSet.Include(rt => rt.User).FirstOrDefaultAsync(rt => rt.RefreshTokenHash == tokenHash);

    /// <inheritdoc />
    public async Task<RefreshTokenEntity?> GetByUserIdAsync(string userId) => await _dbSet.Include(rt => rt.User).FirstOrDefaultAsync(rt => rt.UserId == userId);
}
