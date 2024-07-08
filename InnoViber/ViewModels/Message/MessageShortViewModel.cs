using InnoViber.Domain.Enums;

namespace InnoViber.API.ViewModels.Message;

public class MessageShortViewModel
{
    public string Text { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public Guid ChatId { get; set; }
}
