namespace InnoViber.BLL.Models;

public class ChatModel : BaseModel
{
    public string Name { get; set; } = string.Empty;
    public string? LastMessageText { get; set; }
    public string? LastMessageUserId { get; set; }
    public DateTime? LastMessageDate { get; set; }
    public List<ChatRoleModel> Roles { get; set; } = new();
    public List<MessageModel> Messages { get; set; } = new();
}
