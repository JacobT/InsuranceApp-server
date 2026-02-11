using InsuranceApp.Data.Models;
using InsuranceApp.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InsuranceApp.Data.Repositories;

/// <summary>
/// Provides data access for <see cref="InsuranceClaim"/> entities.
/// </summary>
public class InsuranceClaimRepository : BaseRepository<InsuranceClaim>, IInsuranceClaimRepository
{
    public InsuranceClaimRepository(InsuranceDbContext context) : base(context)
    {
    }

    /// <inheritdoc />
    /// <remarks>
    /// This implementation filters <see cref="InsuranceClaim"/> entities by their 
    /// <see cref="InsuranceClaim.InsuranceId"/> property, which represents the parent 
    /// <see cref="Insurance"/> identifier.
    /// </remarks>
    public async Task<IList<InsuranceClaim>> GetByParentIdAsync(int insuranceId) => await _dbSet.Where(c => c.InsuranceId == insuranceId).ToListAsync();
}
