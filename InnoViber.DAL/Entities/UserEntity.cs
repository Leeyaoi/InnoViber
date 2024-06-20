namespace InnoViber.DAL.Entities;

public class UserEntity : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public List<MessageEntity> Messages { get; set; } = new();
    public List<ChatRoleEntity> Roles { get; set; } = new();
}
