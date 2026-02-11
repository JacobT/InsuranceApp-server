using InsuranceApp.Data.Models.Interfaces;

namespace InsuranceApp.Data.Repositories.Interfaces;

/// <summary>
/// Generic repository providing basic CRUD operations for entities of type <typeparamref name="TEntity"/>.
/// This implementation uses Entity Framework Core's DbContext and DbSet to perform data access.
/// </summary>
/// <typeparam name="TEntity">Entity type managed by this repository. Must be a class.</typeparam>
public interface IBaseRepository<TEntity> where TEntity : class, IEntity
{
    /// <summary>
    /// Retrieves all entities of type <typeparamref name="TEntity"/> asynchronously.
    /// </summary>
    /// <returns>A list containing all entities.</returns>
    Task<IList<TEntity>> GetAllAsync();

    /// <summary>
    /// Finds an entity by its primary key asynchronously.
    /// </summary>
    /// <param name="id">Primary key of the entity.</param>
    /// <returns>The entity if found; otherwise, null.</returns>
    Task<TEntity?> FindByIdAsync(int id);

    /// <summary>
    /// Adds a new entity to the database context asynchronously.
    /// Note: Changes are not persisted until <see cref="SaveChangesAsync"/> is called.
    /// </summary>
    /// <param name="entity">Entity to add.</param>
    /// <returns>The added entity.</returns>
    Task<TEntity> InsertAsync(TEntity entity);

    /// <summary>
    /// Updates the existing entity with values from a modified entity.
    /// Note: Changes are not persisted until <see cref="SaveChangesAsync"/> is called.
    /// </summary>
    /// <param name="dbEntity">The tracked entity retrieved from the database.</param>
    /// <param name="modifiedEntity">The entity containing updated values.</param>
    /// <returns>The updated entity instance.</returns>
    TEntity Update(TEntity dbEntity, TEntity modifiedEntity);

    /// <summary>
    /// Removes the specified entity from the database context.
    /// Note: Changes are not persisted until <see cref="SaveChangesAsync"/> is called.
    /// </summary>
    /// <param name="entity">Entity to remove.</param>
    void Delete(TEntity entity);

    /// <summary>
    /// Saves all changes made in this context to the database asynchronously.
    /// 
    /// Note: This method does not implement concurrency control (e.g., optimistic concurrency).
    /// In a production environment, consider handling DbUpdateConcurrencyException 
    /// or implementing concurrency tokens to manage concurrent updates safely.
    /// </summary>
    Task SaveChangesAsync();
}
