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
    private readonly IMessageService _messageService;

    public ChatService(IMapper mapper, IChatRepository repository, IMessageService messageService) : base(mapper, repository)
    {
        _messageService = messageService;
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<List<ChatModel>> GetByUserId(string userId, CancellationToken ct)
    {
        var entities = await _repository.GetByPredicate(chat => chat.Roles.Any(role => role.UserId == userId), ct);
        var models = _mapper.Map<List<ChatModel>>(entities);
        return await GetLastMessages(models, ct);
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
        models = await GetLastMessages(models, ct);
        return new PaginatedModel<ChatModel>
        {
            Total = total,
            Limit = limit,
            Page = page,
            Count = count,
            Items = models
        };
    }

    private async Task<List<ChatModel>> GetLastMessages(List<ChatModel> chats, CancellationToken ct)
    {
        foreach(var chat in chats)
        {
            var lastMessage = await _messageService.PaginateByChatId(chat.Id, 1, 1, ct);

            if (lastMessage is not null)
            {
                chat.LastMessageText = lastMessage.Items![0].Text;
                chat.LastMessageUserId = lastMessage.Items![0].UserId;
                chat.LastMessageDate = lastMessage.Items![0].Date;
            }
        }

        return chats.OrderByDescending(x => x.LastMessageDate ?? DateTime.UnixEpoch).ToList();
    }
}
