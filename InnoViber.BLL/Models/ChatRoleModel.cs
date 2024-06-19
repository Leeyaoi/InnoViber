using InnoViber.DAL.Entities;
using InnoViber.Domain.Enums;

namespace InnoViber.BLL.Models;

public class ChatRoleModel : BaseModel
{
    public UserRoles Role { get; set; }
    public Guid UserId { get; set; }
    public Guid ChatId { get; set; }
    public UserModel? User { get; set; }
    public ChatModel? Chat { get; set; }
}
