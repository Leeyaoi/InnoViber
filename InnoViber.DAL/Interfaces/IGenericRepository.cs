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
    Task<List<TEntity>> GetAll(CancellationToken ct);

    Task<TEntity> Create(TEntity entity, CancellationToken ct);

    Task<TEntity> Update(TEntity entity, CancellationToken ct);

    Task<TEntity?> GetByPredicate(Expression<Func<TEntity, bool>> predicate, CancellationToken ct);

    Task<TEntity?> GetById(Guid Id, CancellationToken ct);

    Task Delete(TEntity entity, CancellationToken ct);
}
