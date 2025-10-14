using InsuranceApp.Api.Models;
using InsuranceApp.Data.Models;

namespace InsuranceApp.Api.Managers.Interfaces;

/// <summary>
/// Defines a contract for application-layer managers that provide 
/// CRUD operations for entities, using DTOs for data transfer.
/// </summary>
/// <typeparam name="TEntity">
/// The domain entity type that implements <see cref="IEntity"/>.
/// </typeparam>
/// <typeparam name="TDto">
/// The Data Transfer Object (DTO) type corresponding to <typeparamref name="TEntity"/>.
/// </typeparam>
public interface IBaseManager<TEntity, TDto>
    where TEntity : class, IEntity
    where TDto : class, IDto
{
    /// <summary>
    /// Retrieves all <typeparamref name="TDto"/> instances asynchronously.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation.  
    /// The task result contains a list of <typeparamref name="TDto"/> instances.
    /// </returns>
    Task<IList<TDto>> GetAllAsync();

    /// <summary>
    /// Finds a <typeparamref name="TDto"/> by its unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the entity.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.  
    /// The task result contains the <typeparamref name="TDto"/> if found; otherwise, <c>null</c>.
    /// </returns>
    Task<TDto?> FindByIdAsync(int id);

    /// <summary>
    /// Inserts a new <typeparamref name="TDto"/> asynchronously.
    /// </summary>
    /// <param name="dto">The DTO representing the entity to insert.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.  
    /// The task result contains the newly created <typeparamref name="TDto"/>.
    /// </returns>
    Task<TDto> InsertAsync(TDto dto);

    /// <summary>
    /// Updates an existing <typeparamref name="TDto"/> asynchronously.
    /// </summary>
    /// <param name="updatedEntityDto">The DTO containing updated values.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.  
    /// The task result contains the updated <typeparamref name="TDto"/> if the entity exists; otherwise, <c>null</c>.
    /// </returns>
    Task<TDto?> UpdateAsync(TDto updatedEntityDto);

    /// <summary>
    /// Deletes a <typeparamref name="TDto"/> asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the entity to delete.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.  
    /// The task result contains the deleted <typeparamref name="TDto"/> if the entity exists; otherwise, <c>null</c>.
    /// </returns>
    Task<TDto?> DeleteAsync(int id);
}
