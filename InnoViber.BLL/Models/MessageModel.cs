namespace InnoViber.BLL.Models;

public class MessageModel : BaseModel
{
    public DateTime Date { get; set; }
    public string Text { get; set; } = string.Empty;
    public int Status { get; set; }
    public UserModel? UserId { get; set; }
    public ChatModel? ChatId { get; set; }
}
