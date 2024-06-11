using InnoViber.DAL.Models;
using InnoViber.Domain.Enums;

namespace InnoViber.BLL.Models;

public class MessageViewModel : BaseViewModel
{
    public DateTime Date { get; set; }
    public string Text { get; set; } = string.Empty;
    public MessageStatus Status {  get; set; }
}
