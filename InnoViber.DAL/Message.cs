namespace InnoViber.DAL;

public class Message : BaseEntity
{
    public DateTime Date {  get; set; }
    public string Text { get; set; } = string.Empty;
    public int Status { get; set; }
    public User? User { get; set; }
    public Chat? Chat { get; set; }
}
