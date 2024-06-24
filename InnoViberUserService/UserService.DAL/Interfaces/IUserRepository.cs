using MongoDB.Bson;
using System.Linq.Expressions;
using UserService.DAL.Entities;

namespace UserService.DAL.Interfaces;

public interface IUserRepository
{
    Task<List<UserEntity>> GetAll(CancellationToken ct);

    Task<UserEntity> GetById(ObjectId Id, CancellationToken ct);

    Task<UserEntity> GetByPredicate(Expression<Func<UserEntity, bool>> predicate, CancellationToken ct);

    Task<UserEntity> Create(UserEntity entity, CancellationToken ct);

    Task<UserEntity> Update(UserEntity entity, CancellationToken ct);

    Task Delete(UserEntity entity, CancellationToken ct);
}
