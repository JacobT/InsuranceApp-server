using InsuranceApp.Data.Repositories.Interfaces;

namespace InsuranceApp.Api.Services.Interfaces;

/// <summary>
/// Defines a contract for application-layer services that provide 
/// retrieval of a entities based on a search filter.
/// </summary>
/// <typeparam name="TEntity">
/// The domain entity type.
/// </typeparam>
/// <typeparam name="TRepository">
/// The repository type that implements <see cref="IFilterRepository{TEntity}"/>.
/// </typeparam>
public interface IFilterService<TEntity, TRepository>
    where TEntity : class
    where TRepository : IFilterRepository<TEntity>
{
    /// <summary>
    /// Retrieves a all <typeparamref name="TEntity"/>
    /// based on search filter asynchronously.
    /// </summary>
    /// <param name="search">String used for filtering <typeparamref name="TEntity"/> entries</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the a list of <typeparamref name="TEntity"/>.
    /// </returns>
    Task<IList<TEntity>> GetAllAsync(string search);
}
