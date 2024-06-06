namespace InnoViber.BLL.Models;

public class Chat : BaseModel
{
    public string Name { get; set; } = string.Empty;
    List<User> Users { get; set; } = new();
    List<Message> Messages { get; set; } = new();
}
