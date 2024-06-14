using InnoViber.DAL.Models;
using InnoViber.Domain.Enums;

namespace InnoViber.BLL.Models;

public class MessageModel : BaseModel
{
    public DateTime Date { get; set; }
    public string Text { get; set; } = string.Empty;
    public MessageStatus Status { get; set; }
    public Guid UserId { get; set; }
    public Guid ChatId { get; set; }
    public UserEntity? User { get; set; }
    public ChatEntity? Chat { get; set; }

    public bool IsSeen()
    {
        return Status == MessageStatus.Read;
    }
}
