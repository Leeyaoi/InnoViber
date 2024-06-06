using InnoViber.DAL.Data;
using InnoViber.DAL.Interfaces;
using InnoViber.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace InnoViber.DAL.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    protected readonly ViberContext _viberContext;
    protected readonly DbSet<TEntity> _dbSet;

    public GenericRepository(ViberContext context)
    {
        _viberContext = context;
        _dbSet = context.Set<TEntity>();
    }

    public Task<List<TEntity>> GetAll()
    {
        throw new NotImplementedException();
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

}
