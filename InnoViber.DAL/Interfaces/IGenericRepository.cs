using InnoViber.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InnoViber.DAL.Interfaces;

public interface IGenericRepository<TEntity>
{
    public Task<List<TEntity>> GetAll();

    public Task Create(TEntity entity, CancellationToken ct);

    public Task<TEntity> Update(TEntity entity, CancellationToken ct);

    public Task<TEntity?> GetByPredicate(Expression<Func<TEntity, bool>> predicate);

    public Task<TEntity?> GetById(Guid Id);

    public Task Delete(TEntity entity, CancellationToken ct);
}
