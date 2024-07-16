using InnoViber.DAL.Entities;
using InnoViber.Domain.Enums;

namespace InnoViber.BLL.Models;

public class ChatRoleModel : BaseModel
{
    public UserRoles Role { get; set; }
    public string UserId { get; set; } = string.Empty;
    public Guid ChatId { get; set; }
    public ChatModel? Chat { get; set; }
}
