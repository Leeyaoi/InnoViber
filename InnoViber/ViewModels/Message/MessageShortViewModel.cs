using InnoViber.Domain.Enums;

namespace InnoViber.API.ViewModels.Message;

public class MessageShortViewModel
{
    public DateTime Date { get; set; }
    public string Text { get; set; } = string.Empty;
    public MessageStatus Status { get; set; }
    public Guid UserId { get; set; }
    public Guid ChatId { get; set; }
}
