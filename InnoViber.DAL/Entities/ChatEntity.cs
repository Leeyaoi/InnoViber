namespace InnoViber.DAL.Models;

public class ChatEntity : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public List<UserEntity> Users { get; set; } = new();
    public List<MessageEntity> Messages { get; set; } = new();
}
