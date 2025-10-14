using InsuranceApp.Data.Models;
using InsuranceApp.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceApp.Data.Repositories.Interfaces;

/// <summary>
/// Defines a contract for repositories that can retrieve detailed information 
/// about a specific <typeparamref name="TEntity"/>.
/// </summary>
/// <typeparam name="TEntity">
/// The entity type that implements <see cref="IEntity"/>.
/// </typeparam>
public interface IDetailRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IEntity
{
    /// <summary>
    /// Retrieves a single <typeparamref name="TEntity"/> by its identifier, 
    /// including any related data required for a detailed view.
    /// </summary>
    /// <param name="id">The unique identifier of the entity to retrieve.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. 
    /// The task result contains the matching <typeparamref name="TEntity"/> if found; 
    /// otherwise, <c>null</c>.
    /// </returns>
    Task<TEntity?> GetDetailAsync(int id);
}
