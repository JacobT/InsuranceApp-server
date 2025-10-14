using InsuranceApp.Data.Models;
using InsuranceApp.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceApp.Data.Repositories;

/// <summary>
/// Provides data access for <see cref="Insurance"/> entities.
/// </summary>
public class InsuranceRepository : BaseRepository<Insurance>, IInsuranceRepository
{
    public InsuranceRepository(InsuranceDbContext context) : base(context)
    {
    }

    /// <inheritdoc />
    /// <remarks>
    /// This implementation filters <see cref="Insurance"/> entities by their 
    /// <see cref="Insurance.InsuredId"/> property, which represents the parent 
    /// <see cref="Customer"/> identifier.
    /// </remarks>
    public async Task<IList<Insurance>> GetByParentIdAsync(int customerId) => await _dbSet.Where(i => i.InsuredId == customerId).ToListAsync();

    /// <inheritdoc />
    /// <remarks>
    /// This implementation eagerly loads the <see cref="Insurance.Claims"/> 
    /// navigation property when retrieving a insurance.
    /// </remarks>
    public async Task<Insurance?> GetDetailAsync(int insuranceId) => 
        await _dbSet.Include(i => i.Claims).FirstOrDefaultAsync(i => i.Id == insuranceId);
}
