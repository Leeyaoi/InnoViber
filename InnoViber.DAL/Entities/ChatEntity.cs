namespace InnoViber.DAL.Entities;

public class ChatEntity : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public List<ChatRoleEntity> Roles { get; set; } = new();
    public List<MessageEntity> Messages { get; set; } = new();
}
