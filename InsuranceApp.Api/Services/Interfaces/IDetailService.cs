using InsuranceApp.Data.Models;
using InsuranceApp.Data.Repositories.Interfaces;

namespace InsuranceApp.Api.Services.Interfaces;

/// <summary>
/// Defines a contract for application-layer services that provide 
/// detailed retrieval of a single entity.
/// </summary>
/// <typeparam name="TEntity">
/// The domain entity type that implements <see cref="IEntity"/>.
/// </typeparam>
/// <typeparam name="TRepository">
/// The repository type that implements <see cref="IDetailRepository{TEntity}"/>.
/// </typeparam>
public interface IDetailService<TEntity, TRepository>
    where TEntity : class, IEntity
    where TRepository : IDetailRepository<TEntity>
{
    /// <summary>
    /// Retrieves a single <typeparamref name="TEntity"/> with additional details 
    /// by its unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the entity to retrieve.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.  
    /// The task result contains the <typeparamref name="TEntity"/> if found; otherwise, <c>null</c>.
    /// </returns>
    Task<TEntity?> GetDetailAsync(int id);
}
