using AutoMapper;
using InsuranceApp.Api.Managers.Interfaces;
using InsuranceApp.Api.Models.Interfaces;
using InsuranceApp.Data.Models.Interfaces;
using InsuranceApp.Data.Repositories.Interfaces;

namespace InsuranceApp.Api.Managers;

/// <summary>
/// Provides common CRUD operations for entities using a repository and AutoMapper.
/// Implements <see cref="IBaseManager{TEntity, TDto}"/>.
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
/// <typeparam name="TDto">The type of the DTO.</typeparam>
public abstract class BaseManager<TEntity, TDto> : IBaseManager<TEntity, TDto>
    where TEntity : class, IEntity
    where TDto : class, IDto
{
    protected readonly IBaseRepository<TEntity> _repository;
    protected readonly IMapper _mapper;

    public BaseManager(IBaseRepository<TEntity> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<IList<TDto>> GetAllAsync() =>
        _mapper.Map<IList<TDto>>(await _repository.GetAllAsync());

    /// <inheritdoc />
    public async Task<TDto?> FindByIdAsync(int id) =>
        _mapper.Map<TDto>(await _repository.FindByIdAsync(id));

    /// <inheritdoc />
    /// <remarks>
    /// The entity ID is reset to 0 before insertion to ensure a new record is created.
    /// Changes are explicitly saved after insertion.
    /// </remarks>
    public async Task<TDto> InsertAsync(TDto entityDTO)
    {
        TEntity newEntity = _mapper.Map<TEntity>(entityDTO);
        newEntity.Id = 0;
        TEntity createdEntity = await _repository.InsertAsync(newEntity);
        // Explicitly persist changes because the repository does not auto-save
        await _repository.SaveChangesAsync();

        return _mapper.Map<TDto>(createdEntity);
    }

    /// <inheritdoc />
    /// <remarks>
    /// Returns null if the entity with the specified ID does not exist.
    /// Changes are explicitly saved after updating.
    /// </remarks>
    public async Task<TDto?> UpdateAsync(TDto updatedEntityDTO)
    {
        TEntity? existingEntity = await _repository.FindByIdAsync(updatedEntityDTO.Id);
        if (existingEntity is null)
            return null;

        TEntity entityUpdateValues = _mapper.Map<TEntity>(updatedEntityDTO);
        TEntity updatedEntity = _repository.Update(existingEntity, entityUpdateValues);
        // Explicitly persist changes because the repository does not auto-save
        await _repository.SaveChangesAsync();

        return _mapper.Map<TDto>(updatedEntity);
    }

    /// <inheritdoc />
    /// <remarks>
    /// Returns null if the entity with the specified ID does not exist.
    /// Changes are explicitly saved after deletion.
    /// </remarks>
    public async Task<TDto?> DeleteAsync(int id)
    {
        TEntity? existingEntity = await _repository.FindByIdAsync(id);
        if (existingEntity is null)
            return null;

        TDto deletedEntityDTO = _mapper.Map<TDto>(existingEntity);
        _repository.Delete(existingEntity);
        // Explicitly persist changes because the repository does not auto-save
        await _repository.SaveChangesAsync();

        return deletedEntityDTO;
    }
}
