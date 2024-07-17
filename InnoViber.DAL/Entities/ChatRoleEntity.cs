using InnoViber.Domain.Enums;

namespace InnoViber.DAL.Entities;

public class ChatRoleEntity : BaseEntity
{
    public UserRoles Role { get; set; }
    public string UserId { get; set; } = string.Empty;
    public Guid ChatId { get; set; }
    public ChatEntity? Chat { get; set; }
}
