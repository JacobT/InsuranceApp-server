using InsuranceApp.Api.Services.Interfaces;
using InsuranceApp.Data.Models.Interfaces;
using InsuranceApp.Data.Repositories.Interfaces;

namespace InsuranceApp.Api.Services;

/// <summary>
/// Provides an application-layer service for retrieving child entities 
/// associated with a given parent entity by delegating to repositories.
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
public class RelatedEntityService<TParent, TChild, TParentRepository, TChildRepository> : IRelatedEntityService<TParent, TChild, TParentRepository, TChildRepository>
    where TParent : class, IEntity
    where TChild : class, IEntity
    where TParentRepository : IBaseRepository<TParent>
    where TChildRepository : IChildRepository<TChild>
{
    private readonly TParentRepository _parentRepository;
    private readonly TChildRepository _childRepository;

    public RelatedEntityService(TParentRepository parentRepository, TChildRepository childRepository)
    {
        _parentRepository = parentRepository;
        _childRepository = childRepository;
    }

    /// <inheritdoc />
    /// <remarks>
    /// This implementation first verifies that the parent entity exists 
    /// before retrieving its associated child entities.  
    /// If the parent does not exist, the method returns <c>null</c>.
    /// </remarks>
    public async Task<IList<TChild>?> GetByParentId(int parentId)
    {
        TParent? parent = await _parentRepository.FindByIdAsync(parentId);
        if (parent is null)
            return null;

        return await _childRepository.GetByParentIdAsync(parentId);
    }
}
