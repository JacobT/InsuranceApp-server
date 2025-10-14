using InsuranceApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceApp.Data.Repositories.Interfaces;

/// <summary>
/// Defines a contract for repositories that can retrieve child entities 
/// associated with a specific parent entity.
/// </summary>
/// <typeparam name="TEntity">
/// The child entity type that implements <see cref="IEntity"/>.
/// </typeparam>
public interface IChildRepository<TEntity> where TEntity : class, IEntity
{
    /// <summary>
    /// Retrieves all <typeparamref name="TEntity"/> instances associated with the specified parent identifier.
    /// </summary>
    /// <param name="parentId">
    /// The unique identifier of the parent entity whose children should be retrieved.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation.  
    /// The task result contains a list of <typeparamref name="TEntity"/> instances 
    /// associated with the specified parent; the list will be empty if none are found.
    /// </returns>
    Task<IList<TEntity>> GetByParentIdAsync(int parentId);
}
