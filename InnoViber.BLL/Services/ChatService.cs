using AutoMapper;
using InnoViber.BLL.Interfaces;
using InnoViber.BLL.Models;
using InnoViber.DAL.Interfaces;
using InnoViber.DAL.Entities;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace InnoViber.BLL.Services;

public class ChatService : GenericService<ChatModel, ChatEntity>, IChatService
{
    private readonly IMapper _mapper;
    private readonly IChatRepository _repository;
    private readonly IMessageService _messageService;
    private readonly IChatRoleService _chatRoleService;

    public ChatService(IMapper mapper, IChatRepository repository, IMessageService messageService, IChatRoleService chatRoleService) : base(mapper, repository)
    {
        _messageService = messageService;
        _mapper = mapper;
        _repository = repository;
        _chatRoleService = chatRoleService;
    }

    public async Task<List<ChatModel>> GetByUserId(string userId, CancellationToken ct)
    {
        var entities = await _repository.GetByPredicate(chat => chat.Roles.Any(role => role.UserId == userId), ct);
        var models = _mapper.Map<List<ChatModel>>(entities);
        models = await GetLastActivity(models, ct, userId);
        return await GetLastMessages(models, ct, userId);
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
        models = await GetLastActivity(models, ct, userId);
        models = await GetLastMessages(models, ct, userId);
        return new PaginatedModel<ChatModel>
        {
            Total = total,
            Limit = limit,
            Page = page,
            Count = count,
            Items = models
        };
    }

    private async Task<List<ChatModel>> GetLastMessages(List<ChatModel> chats, CancellationToken ct, string userId)
    {
        foreach(var chat in chats)
        {
            var lastMessage = await _messageService.PaginateByChatId(chat.Id, 1, 1, ct, userId, false);

            if (lastMessage is not null && lastMessage.Items!.Count != 0)
            {
                chat.LastMessage = lastMessage.Items[0];
            }
        }

        return chats.OrderByDescending(x => x.LastMessage.Date).ToList();
    }

    private async Task<List<ChatModel>> GetLastActivity(List<ChatModel> chats, CancellationToken ct, string userId)
    {
        foreach (var chat in chats)
        {
            var lastActivity = (await _chatRoleService.GetByChatId(chat.Id, ct)).Find(r => r.UserId == userId)?.LastActivity;

            if (lastActivity is not null)
            {
                chat.LastActivity = lastActivity;
            }
        }

        return chats;
    }
}
