using AutoMapper;
using InnoViber.BLL.Interfaces;
using InnoViber.BLL.Models;
using InnoViber.DAL.Interfaces;
using InnoViber.DAL.Entities;

namespace InnoViber.BLL.Services;

public class ChatService : GenericService<ChatModel, ChatEntity>, IChatService
{
    private readonly IMapper _mapper;
    private readonly IChatRepository _repository;
    public ChatService(IMapper mapper, IChatRepository repository) : base(mapper, repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<List<ChatModel>> GetByUserId(string userId, CancellationToken ct)
    {
        var entities = await _repository.GetByPredicate(chat => chat.Roles.Any(role => role.UserId == userId), ct);
        return _mapper.Map<List<ChatModel>>(entities);
    }

    public async Task<PaginatedModel<ChatModel>> PaginateByUserId(string userId, int limit, int page, CancellationToken ct)
    {
        var entities = await _repository.PaginateByUserId(userId, limit, page, ct, out int total);
        var models = _mapper.Map<List<ChatModel>>(entities);
        int count = total / limit;
        if(total % limit != 0)
        {
            count++;
        }
        return new PaginatedModel<ChatModel>
        {
            Limit = limit,
            Page = page,
            Total = total,
            Count = count,
            Items = models
        };
    }
}
