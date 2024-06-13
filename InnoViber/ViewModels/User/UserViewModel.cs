using InnoViber.API.ViewModels.Chat;
using InnoViber.API.ViewModels.Message;

namespace InnoViber.API.ViewModels.User;

public class UserViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public List<MessageViewModel> Messages { get; set; } = new();
    public List<ChatViewModel> Chats { get; set; } = new();
}
