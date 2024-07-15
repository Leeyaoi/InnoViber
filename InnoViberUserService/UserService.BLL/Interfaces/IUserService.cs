using MongoDB.Bson;
using System.Linq.Expressions;
using UserService.BLL.Models;

namespace UserService.BLL.Interfaces;

public interface IUserService
{
    Task<List<UserModel>> GetAll(CancellationToken ct);

    Task<UserModel?> GetById(Guid id, CancellationToken ct);

    Task<UserModel?> GetByPredicate(Expression<Func<UserModel, bool>> predicate, CancellationToken ct);

    Task<UserModel> GetByAuthId(string authId, CancellationToken ct);

    Task<UserModel> Create(UserModel model, CancellationToken ct);

    Task Delete(Guid id, CancellationToken ct);

    Task<UserModel> Update(Guid id, UserModel model, CancellationToken ct);
}
