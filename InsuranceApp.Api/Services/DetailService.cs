using InsuranceApp.Api.Services.Interfaces;
using InsuranceApp.Data.Models;
using InsuranceApp.Data.Repositories.Interfaces;

namespace InsuranceApp.Api.Services;

/// <summary>
/// Provides an application-layer service for retrieving detailed information 
/// about a specific entity by delegating to a corresponding repository.
/// </summary>
/// <typeparam name="TEntity">
/// The domain entity type that implements <see cref="IEntity"/>.
/// </typeparam>
/// <typeparam name="TRepository">
/// The repository type that implements <see cref="IDetailRepository{TEntity}"/>.
/// </typeparam>
public class DetailService<TEntity, TRepository> : IDetailService<TEntity, TRepository>
    where TEntity : class, IEntity
    where TRepository : IDetailRepository<TEntity>
{
    private readonly TRepository _repository;

    public DetailService(TRepository repository) => _repository = repository;

    /// <inheritdoc/>
    public async Task<TEntity?> GetDetailAsync(int id)
    {
        TEntity? entity = await _repository.GetDetailAsync(id);
        if (entity is null)
            return null;

        return entity;
    }
}
