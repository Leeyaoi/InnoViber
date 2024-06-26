using AutoMapper;
using UserService.ExternalUsers.DAL.Interfaces;
using UserService.ExternalUsers.DAL.Models;
using System.Linq.Expressions;
using UserService.BLL.Interfaces;
using UserService.BLL.Models;
using UserService.DAL.Entities;
using UserService.DAL.Interfaces;

namespace UserService.BLL.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;
    private readonly IUserHttpService _userHttpService;

    public UserService(IUserRepository repository, IMapper mapper, IUserHttpService userHttpService)
    {
        _repository = repository;
        _mapper = mapper;
        _userHttpService = userHttpService;
    }

    public async Task<List<UserModel>> GetAll(CancellationToken ct)
    {
        var result = await _repository.GetAll(ct);
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

    public async Task<UserModel> Create(UserModel model, CancellationToken ct)
    {
        var entity = _mapper.Map<UserEntity>(model);
        var result = await _repository.Create(entity, ct);
        await _userHttpService.CreateUser(new ExternalUserModel() { MongoId = result.Id }, ct);
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
}
