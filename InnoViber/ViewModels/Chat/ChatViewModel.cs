using InnoViber.API.ViewModels.ChatRole;
using InnoViber.API.ViewModels.Message;

namespace InnoViber.API.ViewModels.Chat;

public class ChatViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public MessageViewModel LastMessage { get; set; } = new();
    public DateTime? LastActivity { get; set; }
    List<ChatRoleViewModel> Roles { get; set; } = new();
    List<MessageViewModel> Messages { get; set; } = new();
}
