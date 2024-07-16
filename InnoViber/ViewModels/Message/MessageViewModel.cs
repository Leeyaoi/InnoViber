using InnoViber.API.ViewModels.Chat;
using InnoViber.Domain.Enums;

namespace InnoViber.API.ViewModels.Message;

public class MessageViewModel
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public string Text { get; set; } = string.Empty;
    public MessageStatus Status { get; set; }
    public string UserId { get; set; } = string.Empty;
    public Guid ChatId { get; set; }
    public ChatViewModel? Chat { get; set; }
}
