using AutoMapper;
using InnoViber.BLL.Interfaces;
using InnoViber.BLL.Models;
using InnoViber.DAL.Interfaces;
using InnoViber.DAL.Entities;
using InnoViber.Domain.Providers;
using InnoViber.Domain.Enums;

namespace InnoViber.BLL.Services;

public class ChatRoleService : GenericService<ChatRoleModel, ChatRoleEntity>, IChatRoleService
{
    private readonly IMapper _mapper;
    private readonly IGenericRepository<ChatRoleEntity> _repository;

    public ChatRoleService(IMapper mapper, IChatRoleRepository repository) : base(mapper, repository) 
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<List<ChatRoleModel>> GetByChatId(Guid chatId, CancellationToken ct)
    {
        var entities = await _repository.GetByPredicate(role => role.ChatId == chatId, ct);
        return _mapper.Map<List<ChatRoleModel>>(entities);
    }

    public async Task<ChatRoleModel> UpdateRole(UserRoles role, ChatRoleModel model, CancellationToken ct)
    {
        model.Role = role;
        var entity = _mapper.Map<ChatRoleEntity>(model);
        var result = await _repository.Update(entity, ct);
        return _mapper.Map<ChatRoleModel>(result);
    }
}
