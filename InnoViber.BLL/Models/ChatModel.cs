namespace InnoViber.BLL.Models;

public class ChatModel : BaseModel
{
    public string Name { get; set; } = string.Empty;
    public MessageModel LastMessage { get; set; } = new();
    public DateTime? LastActivity { get; set; }
    public List<ChatRoleModel> Roles { get; set; } = new();
    public List<MessageModel> Messages { get; set; } = new();
}
