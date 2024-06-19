using InnoViber.DAL.Entities;
using InnoViber.Domain.Enums;

namespace InnoViber.BLL.Models;

public class MessageModel : BaseModel
{
    public DateTime Date { get; set; }
    public string Text { get; set; } = string.Empty;
    public MessageStatus Status { get; set; }
    public Guid UserId { get; set; }
    public Guid ChatId { get; set; }
    public UserModel? User { get; set; }
    public ChatModel? Chat { get; set; }

    public bool IsSeen()
    {
        return Status == MessageStatus.Read;
    }
}
