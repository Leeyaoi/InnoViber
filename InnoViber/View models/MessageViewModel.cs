using InnoViber.DAL.Models;

namespace InnoViber.BLL.Models;

public class MessageViewModel : BaseViewModel
{
    public DateTime Date { get; set; }
    public string Text { get; set; } = string.Empty;
    public enum Status;
}
