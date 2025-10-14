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
/// Provides data access for <see cref="Customer"/> entities.
/// </summary>
public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(InsuranceDbContext context) : base(context)
    {
    }

    /// <inheritdoc />
    /// <remarks>
    /// This implementation eagerly loads the <see cref="Customer.Insurances"/> 
    /// navigation property when retrieving a customer.
    /// </remarks>
    public async Task<Customer?> GetDetailAsync(int customerId) => 
        await _dbSet.Include(c => c.Insurances).FirstOrDefaultAsync(c => c.Id == customerId);
}
