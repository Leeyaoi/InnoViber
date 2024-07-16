using AutoMapper;
using InnoViber.BLL.Interfaces;
using InnoViber.BLL.Models;
using InnoViber.DAL.Interfaces;
using InnoViber.DAL.Entities;
using InnoViber.User.DAL.Models;
using InnoViber.User.DAL.Interfaces;
using System.Net.Http.Json;

namespace InnoViber.BLL.Services;

public class UserService : GenericService<UserModel, UserEntity>, IUserService
{
    private readonly IMapper _mapper;
    private readonly IGenericRepository<UserEntity> _repository;
    private readonly IUserHttpService _userHttpService;

    public UserService(IMapper mapper, IUserRepository repository, IUserHttpService userHttpService) : base(mapper, repository)
    {
        _mapper = mapper;
        _repository = repository;
        _userHttpService = userHttpService;
    }

    public async Task<List<UserModel>> GetByMongoId(Guid mongoId, CancellationToken ct)
    {
        var entity = await _repository.GetByPredicate(user => user.MongoId == mongoId, ct);
        return _mapper.Map<List<UserModel>>(entity);
    }

    public async Task<ExternalUserModel> GetOrCreate(ShortExternalUserModel userModel, CancellationToken ct)
    {
        var user = await _userHttpService.GetUserByAuthId(userModel.Auth0Id, ct);
        if (user == null) {
            var userContent = await (await _userHttpService.PostUser(userModel, ct)).Content.ReadFromJsonAsync<ExternalUserModel>();
            await _repository.Create(new UserEntity { MongoId = userContent!.Id }, ct);
            user = userContent;
        }
        return user;
    }
}
