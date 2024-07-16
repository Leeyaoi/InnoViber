using InnoViber.API.ViewModels.ChatRole;
using InnoViber.API.ViewModels.Message;

namespace InnoViber.API.ViewModels.Chat;

public class ChatViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    List<ChatRoleViewModel> Roles { get; set; } = new();
    List<MessageViewModel> Messages { get; set; } = new();
}
