using InnoViber.DAL.Data;
using InnoViber.DAL.Interfaces;
using InnoViber.DAL.Models;
using InnoViber.Domain.Providers;
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

    public Task<List<TEntity>> GetAll(CancellationToken ct)
    {
        return _dbSet.ToListAsync(ct);
    }

    public async Task Create(TEntity entity, CancellationToken ct)
    {
        await _dbSet.AddAsync(entity);
        await _viberContext.SaveChangesAsync(ct);
    }

    public async Task<TEntity> Update(TEntity entity, CancellationToken ct)
    {
        _dbSet.Update(entity);
        await _viberContext.SaveChangesAsync(ct);
        return entity;
    }

    public Task Delete(TEntity entity, CancellationToken ct)
    {
        _dbSet.Remove(entity);
        return _viberContext.SaveChangesAsync(ct);
    }

    public Task<TEntity?> GetByPredicate(Expression<Func<TEntity, bool>> predicate, CancellationToken ct)
    {
        return _dbSet.Where(predicate).FirstOrDefaultAsync(ct);
    }

    public virtual Task<TEntity?> GetById(Guid Id, CancellationToken ct)
    {
        return _dbSet.Where(x => x.Id == Id).FirstOrDefaultAsync(ct);
    }
}
