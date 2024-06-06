namespace InnoViber.DAL.Models;

public class Chat : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public List<User> Users { get; set; } = new();
    public List<Message> Messages { get; set; } = new();
}
