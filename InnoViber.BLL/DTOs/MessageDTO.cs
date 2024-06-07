namespace InnoViber.BLL.Models;

public class MessageDTO : BaseDTO
{
    public DateTime Date { get; set; }
    public string Text { get; set; } = string.Empty;
    public int Status { get; set; }
    public UserDTO? UserId { get; set; }
    public ChatDTO? ChatId { get; set; }
}
