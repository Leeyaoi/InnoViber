namespace InnoViber.DAL.Models;

public class Chat : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    List<User> Users { get; set; } = new();
    List<Message> Messages { get; set; } = new();
}
