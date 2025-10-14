using InsuranceApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceApp.Data.Repositories.Interfaces;

/// <summary>
/// Repository contract for accessing and managing <see cref="Insurance"/> entities.
/// Combines basic CRUD operations with detailed retrieval and retrieval by parent id functionality.
/// </summary>
public interface IInsuranceRepository : IBaseRepository<Insurance>, IDetailRepository<Insurance>,
    IChildRepository<Insurance>
{
}
