using InnoViber.DAL.Entities;
using InnoViber.Domain.Enums;
using System.Linq.Expressions;

namespace InnoViber.BLL.Models;

public class MessageModel : BaseModel
{
    public DateTime Date { get; set; }
    public string Text { get; set; } = string.Empty;
    public MessageStatus Status { get; set; }
    public string UserId { get; set; } = string.Empty;
    public Guid ChatId { get; set; }
    public ChatModel? Chat { get; set; }

    public bool IsSeen { get
        {
            return Status == MessageStatus.Read;
        }
    }
}
