using InnoViber.API.ViewModels.Chat;
using InnoViber.API.ViewModels.ChatRole;
using InnoViber.API.ViewModels.Message;

namespace InnoViber.API.ViewModels.User;

public class UserViewModel
{
    public Guid Id { get; set; }
    public Guid MongoId { get; set; }
    public List<MessageViewModel> Messages { get; set; } = new();
    public List<ChatRoleViewModel> Roles { get; set; } = new();
}
