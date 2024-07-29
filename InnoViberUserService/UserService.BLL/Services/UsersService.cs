using AutoMapper;
using MongoDB.Bson;
using System.Linq.Expressions;
using UserService.BLL.Interfaces;
using UserService.BLL.Models;
using UserService.DAL.Entities;
using UserService.DAL.Interfaces;

namespace UserService.BLL.Services;

public class UsersService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;

    public UsersService(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<UserModel>> GetAll(CancellationToken ct, string? query)
    {
        var result = await _repository.GetAll(ct, query);
        return _mapper.Map<List<UserModel>>(result);
    }

    public async Task<UserModel?> GetById(Guid id, CancellationToken ct)
    {
        var result = await _repository.GetById(id, ct);
        return _mapper.Map<UserModel>(result);
    }

    public async Task<UserModel?> GetByPredicate(Expression<Func<UserModel, bool>> predicate, CancellationToken ct)
    {
        var entityPred = _mapper.Map<Expression<Func<UserEntity, bool>>>(predicate);
        var result = await _repository.GetByPredicate(entityPred, ct);
        return _mapper.Map<UserModel>(result);
    }

    public async Task<UserModel> GetByAuthId(string authId, CancellationToken ct)
    {
        var result = await _repository.GetByPredicate(user => user.Auth0Id == authId, ct);
        return _mapper.Map<UserModel>(result);
    }

    public async Task<UserModel> Create(UserModel model, CancellationToken ct)
    {
        var entity = _mapper.Map<UserEntity>(model);
        var result = await _repository.Create(entity, ct);
        return _mapper.Map<UserModel>(result);
    }

    public async Task Delete(Guid id, CancellationToken ct)
    {
        var result = await _repository.GetById(id, ct);
        await _repository.Delete(result, ct);
    }

    public async Task<UserModel> Update(Guid id, UserModel model, CancellationToken ct)
    {
        model.Id = id;
        var entity = _mapper.Map<UserEntity>(model);
        var result = await _repository.Update(entity, ct);
        return _mapper.Map<UserModel>(result);
    }

    public async Task<UserModel> GetOrCreate(UserModel model, CancellationToken ct)
    {
        var user = await GetByAuthId(model.Auth0Id, ct);
        if (user == null)
        {
            user = await Create(model, ct);
        }
        else
        {
            user = await Update(user.Id, model, ct);
        }
        return user;
    }

    public async Task<Dictionary<string, UserModel>> GetNames(List<string> ids, CancellationToken ct)
    {
        var users = new Dictionary<string, UserModel>();
        foreach(var id in ids)
        {
            if (!users.Keys.Contains(id))
            {
                var user = await GetByAuthId(id, ct);
                users[id] = user;
            }
        }
        return users;
    }
}
