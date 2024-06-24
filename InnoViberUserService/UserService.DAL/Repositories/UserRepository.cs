using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;
using UserService.DAL.Data;
using UserService.DAL.Entities;
using UserService.DAL.Interfaces;

namespace UserService.DAL.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IMongoCollection<UserEntity> _users;

    public UserRepository(MongoDbContext context)
    {
        _users = context.Database.GetCollection<UserEntity>("users");
    }

    public Task<List<UserEntity>> GetAll(CancellationToken ct)
    {
        return _users.Find(FilterDefinition<UserEntity>.Empty).ToListAsync(ct);
    }

    public Task<UserEntity> GetById(ObjectId Id, CancellationToken ct)
    {
        return _users.Find(x => x.Id == Id).FirstOrDefaultAsync(ct);
    }

    public Task<UserEntity> GetByPredicate(Expression<Func<UserEntity, bool>> predicate, CancellationToken ct)
    {
        return _users.Find(predicate).FirstOrDefaultAsync(ct);
    }

    public async Task<UserEntity> Create(UserEntity entity, CancellationToken ct)
    {
        await _users.InsertOneAsync(entity, cancellationToken: ct);
        return entity;
    }

    public Task Delete(UserEntity entity, CancellationToken ct)
    {
        var filter = Builders<UserEntity>.Filter.Eq(x => x.Id, entity.Id);
        return _users.DeleteOneAsync(filter, ct);
    }

    public async Task<UserEntity> Update(UserEntity entity, CancellationToken ct)
    {
        var filter = Builders<UserEntity>.Filter.Eq(x => x.Id, entity.Id);
        await _users.ReplaceOneAsync(filter, entity, cancellationToken: ct);
        return entity;
    }
}
