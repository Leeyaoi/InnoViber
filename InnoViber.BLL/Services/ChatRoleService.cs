using AutoMapper;
using InnoViber.BLL.Interfaces;
using InnoViber.BLL.Models;
using InnoViber.DAL.Interfaces;
using InnoViber.DAL.Entities;
using InnoViber.Domain.Enums;

namespace InnoViber.BLL.Services;

public class ChatRoleService : GenericService<ChatRoleModel, ChatRoleEntity>, IChatRoleService
{
    private readonly IMapper _mapper;
    private readonly IChatRoleRepository _repository;

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

    public async Task<PaginatedModel<ChatRoleModel>> PaginateByChatId(Guid chatId, int limit, int page, CancellationToken ct)
    {
        var entities = await _repository.PaginateByChatId(chatId, limit, page, ct, out int total);
        var models = _mapper.Map<List<ChatRoleModel>>(entities);
        int count = total / limit;
        if (total % limit != 0)
        {
            count++;
        }
        return new PaginatedModel<ChatRoleModel>
        {
            Total = total,
            Limit = limit,
            Page = page,
            Count = count,
            Items = models
        };
    }

    public async Task<ChatRoleModel> UpdateRole(UserRoles role, ChatRoleModel model, CancellationToken ct)
    {
        model.Role = role;
        var entity = _mapper.Map<ChatRoleEntity>(model);
        var result = await _repository.Update(entity, ct);
        return _mapper.Map<ChatRoleModel>(result);
    }
}
