using System.Diagnostics.CodeAnalysis;

namespace InnoViber.BLL.Models;

public class UserModel : BaseModel
{
    public Guid MongoId { get; set; }
    public List<MessageModel> Messages { get; set; } = new();
    public List<ChatModel> Chats { get; set; } = new();
}
