using InnoViber.BLL.Models;
using InnoViber.Domain.Enums;

namespace InnoViber.API.ViewModels.ChatRole;

public class ChatRoleViewModel
{
    public Guid Id { get; set; }
    public UserRoles Role { get; set; }
    public Guid UserId { get; set; }
    public Guid ChatId { get; set; }
    public UserModel? User { get; set; }
    public ChatModel? Chat { get; set; }
}
