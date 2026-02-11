using InsuranceApp.Data.Models;

namespace InsuranceApp.Data.Repositories.Interfaces;

/// <summary>
/// Repository contract for accessing and managing <see cref="Customer"/> entities.
/// Combines basic CRUD operations with detailed retrieval functionality.
/// </summary>
public interface ICustomerRepository : IBaseRepository<Customer>, IDetailRepository<Customer>, IFilterRepository<Customer>
{
}
