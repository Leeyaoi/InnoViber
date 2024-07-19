using InnoViber.Domain.Enums;

namespace InnoViber.DAL.Entities;

public class MessageEntity : BaseEntity
{
    public DateTime Date { get; set; }
    public string Text { get; set; } = string.Empty;
    public MessageStatus Status {  get; set; }
    public MessageType Type { get; set; } = MessageType.Simple;
    public string UserId{ get; set; } = string.Empty;
    public Guid ChatId{ get; set; }
    public ChatEntity? Chat { get; set; }
}
