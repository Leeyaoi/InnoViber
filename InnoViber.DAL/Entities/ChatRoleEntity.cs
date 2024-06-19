using InnoViber.Domain.Enums;

namespace InnoViber.DAL.Entities;

public class ChatRoleEntity : BaseEntity
{
    public UserRoles Role { get; set; }
    public Guid UserId { get; set; }
    public Guid ChatId { get; set; }
}
