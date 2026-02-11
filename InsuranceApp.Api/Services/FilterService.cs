using InsuranceApp.Api.Services.Interfaces;
using InsuranceApp.Data.Repositories.Interfaces;

namespace InsuranceApp.Api.Services;

/// <summary>
/// Provides an application-layer service for retrieving filtered entries
/// of a specific entity by delegating to a corresponding repository.
/// </summary>
/// <typeparam name="TEntity">
/// The domain entity type.
/// </typeparam>
/// <typeparam name="TRepository">
/// The repository type that implements <see cref="IFilterRepository{TEntity}"/>.
/// </typeparam>
public class FilterService<TEntity, TRepository> : IFilterService<TEntity, TRepository>
    where TEntity : class
    where TRepository : IFilterRepository<TEntity>
{
    private readonly TRepository _repository;

    public FilterService(TRepository repository) => _repository = repository;

    /// <inheritdoc/>
    public async Task<IList<TEntity>> GetAllAsync(string search) =>
        await _repository.GetAllAsync(search);
}
