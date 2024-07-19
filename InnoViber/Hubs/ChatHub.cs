using InnoViber.API.Models;
using InnoViber.API.ViewModels.Message;
using InnoViber.BLL.Interfaces;
using InnoViber.BLL.Models;
using InnoViber.Domain.Enums;
using InnoViber.Domain.Providers;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace InnoViber.API.Hubs;

public interface IChatClient
{
    public Task RecieveMessage(string UserName, MessageViewModel message);
    public Task RecieveAdminMessage(MessageViewModel message, string UserName="Admin");
}

public class ChatHub : Hub<IChatClient>
{
    private readonly IMessageService _messageService;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IMemoryCache _cache;

    public ChatHub(IMessageService messageService, IDateTimeProvider dateTimeProvider, IMemoryCache cache)
    {
        _messageService = messageService;
        _dateTimeProvider = dateTimeProvider;
        _cache = cache;
    }

    public async Task JoinChat(UserConnection connection)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, connection.ChatId);

        var stringConnection = JsonSerializer.Serialize(connection);

        _cache.Set(Context.ConnectionId, stringConnection);

        await Clients.Group(connection.ChatId).RecieveAdminMessage(new MessageViewModel() {
            Type = MessageType.Special,
            Text = $"{connection.UserName} is joined"
        });
        await _messageService.Create(new MessageModel()
        {
            Date = _dateTimeProvider.GetDate(),
            Type = MessageType.Special,
            Text = $"{connection.UserName} is joined",
            ChatId = new Guid(connection.ChatId),
            UserId = "InnoViberAdmin"
        }, default);
    }

    public async Task SendMessage(MessageViewModel message)
    {
        var stringConnection = _cache.Get(Context.ConnectionId).ToString();
        var connection = JsonSerializer.Deserialize<UserConnection>(stringConnection);

        if(connection is not null){ 
            await Clients.OthersInGroup(connection.ChatId).RecieveMessage(connection.UserName, message); 
        }
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var stringConnection = _cache.Get(Context.ConnectionId).ToString();
        var connection = JsonSerializer.Deserialize<UserConnection>(stringConnection);

        if (connection is not null)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, connection.ChatId);
        }

        await base.OnDisconnectedAsync(exception);
    }
}
