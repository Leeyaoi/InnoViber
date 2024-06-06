namespace InnoViber.DAL.Models;

public class Message : BaseEntity
{
    public DateTime Date { get; set; }
    public string Text { get; set; } = string.Empty;
    public int Status { get; set; }
    public User? UserId { get; set; }
    public Chat? ChatId { get; set; }
}
