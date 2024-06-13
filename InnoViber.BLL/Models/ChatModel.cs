namespace InnoViber.BLL.Models;

public class ChatModel : BaseModel
{
    public string Name { get; set; } = string.Empty;
    public Guid OwnerId { get; set; }
    public UserModel Owner { get; set; } = new();
    List<UserModel> Users { get; set; } = new();
    List<MessageModel> Messages { get; set; } = new();
}
