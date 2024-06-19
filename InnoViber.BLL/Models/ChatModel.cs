namespace InnoViber.BLL.Models;

public class ChatModel : BaseModel
{
    public string Name { get; set; } = string.Empty;
    List<UserModel> Users { get; set; } = new();
    List<MessageModel> Messages { get; set; } = new();
}
