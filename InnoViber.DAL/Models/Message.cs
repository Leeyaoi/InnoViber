namespace InnoViber.DAL.Models;

public class Message : BaseEntity
{
    public DateTime Date { get; set; }
    public string Text { get; set; } = string.Empty;
    public int Status { get; set; }
    public Guid UserId{ get; set; }
    public Guid ChatId{ get; set; }
    public User? User { get; set; }
    public Chat? Chat { get; set; }
}
