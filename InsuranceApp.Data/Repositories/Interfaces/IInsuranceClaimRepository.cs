using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsuranceApp.Data.Models;

namespace InsuranceApp.Data.Repositories.Interfaces;

/// <summary>
/// Repository contract for accessing and managing <see cref="InsuranceClaim"/> entities.
/// Combines basic CRUD operations with retrieval by parent id functionality.
/// </summary>
public interface IInsuranceClaimRepository : IBaseRepository<InsuranceClaim>, IChildRepository<InsuranceClaim>
{
}
