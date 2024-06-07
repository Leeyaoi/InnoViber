using System.Diagnostics.CodeAnalysis;

namespace InnoViber.BLL.Models;

public class UserModel : BaseModel
{
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public List<MessageModel> Messages { get; set; } = new();
    public List<ChatModel> Chats { get; set; } = new();
}
