using InsuranceApp.Data.Models.Interfaces;
using InsuranceApp.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace InsuranceApp.Data.Repositories;

/// <inheritdoc/>
public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
    where TEntity : class, IEntity
{
    protected readonly InsuranceDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public BaseRepository(InsuranceDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public async Task<IList<TEntity>> GetAllAsync() => await _dbSet.ToListAsync();

    public async Task<TEntity?> FindByIdAsync(int id) => await _dbSet.FindAsync(id);

    public async Task<TEntity> InsertAsync(TEntity entity)
    {
        EntityEntry<TEntity> addedEntity = await _dbSet.AddAsync(entity);
        return addedEntity.Entity;
    }

    public TEntity Update(TEntity dbEntity, TEntity modifiedEntity)
    {
        _context.Entry(dbEntity).CurrentValues.SetValues(modifiedEntity);
        return dbEntity;
    }

    public void Delete(TEntity entity) => _dbSet.Remove(entity);

    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
}
