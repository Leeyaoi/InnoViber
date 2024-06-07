using System.Linq.Expressions;

namespace InnoViber.BLL.Interfaces;

public interface IGenericService<TModel>
{
    Task<List<TModel>> GetAll(CancellationToken ct);

    Task<TModel?> GetById(Guid id, CancellationToken ct);

    Task<TModel?> GetByPredicate(Expression<Func<TModel, bool>> predicate, CancellationToken ct);

    Task Create(TModel model, CancellationToken ct);

    Task Delete(TModel model, CancellationToken ct);

    Task Update(TModel model, CancellationToken ct);
}
