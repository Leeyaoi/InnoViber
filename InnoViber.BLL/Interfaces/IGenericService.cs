using System.Linq.Expressions;

namespace InnoViber.BLL.Interfaces;

public interface IGenericService<TModel>
{
    Task<List<TModel>> GetAll(CancellationToken ct);

    Task<TModel?> GetById(Guid id, CancellationToken ct);

    Task<List<TModel?>> GetByPredicate(Expression<Func<TModel, bool>> predicate, CancellationToken ct);

    Task<TModel> Create(TModel model, CancellationToken ct);

    Task Delete(Guid id, CancellationToken ct);

    Task<TModel> Update(Guid id, TModel model, CancellationToken ct);
}
