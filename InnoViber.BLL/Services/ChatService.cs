using AutoMapper;
using InnoViber.BLL.Interfaces;
using InnoViber.BLL.Models;
using InnoViber.DAL.Interfaces;
using InnoViber.DAL.Entities;

namespace InnoViber.BLL.Services;

public class ChatService : GenericService<ChatModel, ChatEntity>, IChatService
{
    private readonly IMapper _mapper;
    private readonly IGenericRepository<ChatEntity> _repository;
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
}
