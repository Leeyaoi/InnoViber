using AutoMapper;
using InnoViber.BLL.Interfaces;
using InnoViber.BLL.Models;
using InnoViber.DAL.Interfaces;
using InnoViber.DAL.Entities;

namespace InnoViber.BLL.Services;

public class UserService : GenericService<UserModel, UserEntity>, IUserService
{
    private readonly IMapper _mapper;
    private readonly IGenericRepository<UserEntity> _repository;

    public UserService(IMapper mapper, IUserRepository repository) : base(mapper, repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<List<UserModel>> GetByMongoId(Guid mongoId, CancellationToken ct)
    {
        var entity = await _repository.GetByPredicate(user => user.MongoId == mongoId, ct);
        return _mapper.Map<List<UserModel>>(entity);
    }
}
