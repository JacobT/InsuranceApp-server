using InsuranceApp.Data.Models.Interfaces;
using InsuranceApp.Data.Repositories.Interfaces;

namespace InsuranceApp.Api.Services.Interfaces;

/// <summary>
/// Defines a contract for application-layer services that retrieve child entities 
/// associated with a specified parent entity.
/// </summary>
/// <typeparam name="TParent">
/// The parent entity type that implements <see cref="IEntity"/>.
/// </typeparam>
/// <typeparam name="TChild">
/// The child entity type that implements <see cref="IEntity"/>.
/// </typeparam>
/// <typeparam name="TParentRepository">
/// The repository type that manages parent entities and implements <see cref="IBaseRepository{TEntity}"/>.
/// </typeparam>
/// <typeparam name="TChildRepository">
/// The repository type that manages child entities and implements <see cref="IChildRepository{TEntity}"/>.
/// </typeparam>
public interface IRelatedEntityService<TParent, TChild, TParentRepository, TChildRepository>
    where TParent : class, IEntity
    where TChild : class, IEntity
    where TParentRepository : IBaseRepository<TParent>
    where TChildRepository : IChildRepository<TChild>
{
    /// <summary>
    /// Retrieves all <typeparamref name="TChild"/> entities associated with the specified parent.
    /// </summary>
    /// <param name="parentId">The unique identifier of the parent entity.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.  
    /// The task result contains a list of <typeparamref name="TChild"/> entities 
    /// if the parent exists; otherwise, <c>null</c>.
    /// </returns>
    Task<IList<TChild>?> GetByParentId(int parentId);
}
