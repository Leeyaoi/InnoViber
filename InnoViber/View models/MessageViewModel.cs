using InnoViber.DAL.Models;

namespace InnoViber.BLL.Models;

public class MessageViewModel : BaseViewModel
{
    public DateTime Date { get; set; }
    public string Text { get; set; } = string.Empty;
    public MessageStatus Status {  get; set; }

    public enum MessageStatus
    {
        Send = 0,
        Delivered = 1,
        Read = 2
    }
}
