using InnoViber.API.ViewModels.Message;
using InnoViber.API.ViewModels.User;
using InnoViber.BLL.Models;

namespace InnoViber.API.ViewModels.Chat;

public class ChatViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid OwnerId { get; set; }
    public UserModel Owner { get; set; } = new();
    List<UserViewModel> Users { get; set; } = new();
    List<MessageViewModel> Messages { get; set; } = new();
}
