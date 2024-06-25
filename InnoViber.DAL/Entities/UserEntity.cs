namespace InnoViber.DAL.Entities;

public class UserEntity : BaseEntity
{
    public Guid MongoId { get; set; }
    public List<MessageEntity> Messages { get; set; } = new();
    public List<ChatRoleEntity> Roles { get; set; } = new();
}
