namespace InsuranceApp.Data.Repositories.Interfaces;

/// <summary>
/// Defines a contract for repositories that can retrieve
/// all <typeparamref name="TEntity"/> based on a filter.
/// </summary>
/// <typeparam name="TEntity">
/// The domain entity type.
/// </typeparam>
public interface IFilterRepository<TEntity>
{
    /// <summary>
    /// Retrieves a filtered list of <typeparamref name="TEntity"/> entities.
    /// </summary>
    /// <param name="search">String used for filtering <typeparamref name="TEntity"/> entries.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.  
    /// The task result contains the a list of <typeparamref name="TEntity"/>.
    /// </returns>
    Task<IList<TEntity>> GetAllAsync(string search);
}
