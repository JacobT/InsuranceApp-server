using InsuranceApp.Data.Models;
using InsuranceApp.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InsuranceApp.Data.Repositories;

/// <summary>
/// Provides data access for <see cref="Customer"/> entities.
/// </summary>
public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(InsuranceDbContext context) : base(context)
    {
    }

    /// <inheritdoc/>
    /// <remarks>
    /// This implementation filters entries based on whether <see cref="Customer.FirstName"/>,
    /// <see cref="Customer.LastName"/> or <see cref="Customer.Email"/> includes <paramref name="search"/>.
    /// </remarks>
    public async Task<IList<Customer>> GetAllAsync(string search)
    {
        IQueryable<Customer> query = _dbSet;

        query = query.Where(customer =>
            customer.FirstName.Contains(search) ||
            customer.LastName.Contains(search) ||
            customer.Email.Contains(search));

        return await query.ToListAsync();
    }

    /// <inheritdoc />
    /// <remarks>
    /// This implementation eagerly loads the <see cref="Customer.Insurances"/> 
    /// navigation property when retrieving a customer.
    /// </remarks>
    public async Task<Customer?> GetDetailAsync(int customerId) => 
        await _dbSet.Include(c => c.Insurances).FirstOrDefaultAsync(c => c.Id == customerId);
}
