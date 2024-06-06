using System.Diagnostics.CodeAnalysis;

namespace InnoViber.DAL.Models;

public class User : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public List<Message> Messages { get; set; } = new();
    public List<Chat> Chats { get; set; } = new();
}
