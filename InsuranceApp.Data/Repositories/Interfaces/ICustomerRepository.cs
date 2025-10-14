using InsuranceApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceApp.Data.Repositories.Interfaces;

/// <summary>
/// Repository contract for accessing and managing <see cref="Customer"/> entities.
/// Combines basic CRUD operations with detailed retrieval functionality.
/// </summary>
public interface ICustomerRepository : IBaseRepository<Customer>, IDetailRepository<Customer>
{
}
