using InnoViber.DAL.Data;
using InnoViber.DAL.Interfaces;
using InnoViber.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InnoViber.DAL.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly ViberContext _viberContext;
    protected readonly DbSet<TEntity> _dbSet;

    public GenericRepository(ViberContext context)
    {
        _viberContext = context;
        _dbSet = context.Set<TEntity>();
    }

    public virtual Task<List<TEntity>> GetAll(CancellationToken ct)
    {
        return _dbSet.AsNoTracking().ToListAsync(ct);
    }

    public async Task<TEntity> Create(TEntity entity, CancellationToken ct)
    {
        var result = await _dbSet.AddAsync(entity);
        await _viberContext.SaveChangesAsync(ct);
        return result.Entity;
    }

    public virtual async Task<TEntity> Update(TEntity entity, CancellationToken ct)
    {
        var result = _dbSet.Update(entity);
        await _viberContext.SaveChangesAsync(ct);
        return result.Entity;
    }

    public Task Delete(TEntity entity, CancellationToken ct)
    {
        _dbSet.Remove(entity);
        return _viberContext.SaveChangesAsync(ct);
    }

    public Task<List<TEntity?>> GetByPredicate(Expression<Func<TEntity, bool>> predicate, CancellationToken ct)
    {
        return _dbSet.AsNoTracking().Where(predicate).ToListAsync(ct);
    }

    public virtual Task<TEntity?> GetById(Guid Id, CancellationToken ct)
    {
        return _dbSet.AsNoTracking().Where(x => x.Id == Id).FirstOrDefaultAsync(ct);
    }
}
