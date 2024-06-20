using InnoViber.DAL.Entities;
using InnoViber.Domain.Enums;
using System.Linq.Expressions;

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

    public bool IsSeen { get
        {
            return Status == MessageStatus.Read;
        }
    }
}
