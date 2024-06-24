using MongoDB.Bson;
using System.Linq.Expressions;
using UserService.BLL.Models;

namespace UserService.BLL.Interfaces;

public interface IUserService
{
    Task<List<UserModel>> GetAll(CancellationToken ct);

    Task<UserModel?> GetById(ObjectId id, CancellationToken ct);

    Task<UserModel?> GetByPredicate(Expression<Func<UserModel, bool>> predicate, CancellationToken ct);

    Task<UserModel> Create(UserModel model, CancellationToken ct);

    Task Delete(ObjectId id, CancellationToken ct);

    Task<UserModel> Update(UserModel model, CancellationToken ct);
}
