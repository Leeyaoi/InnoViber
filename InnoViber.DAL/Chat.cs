namespace InnoViber.DAL;

public class Chat : IAuditable
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    List<User> Users { get; set; } = new();
    List<Message> Messages { get; set;} = new();
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
}
