using System.Linq.Expressions;

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
