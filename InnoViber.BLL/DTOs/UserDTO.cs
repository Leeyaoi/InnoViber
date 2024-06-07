using System.Diagnostics.CodeAnalysis;

namespace InnoViber.BLL.Models;

public class UserDTO : BaseDTO
{
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public List<MessageDTO> Messages { get; set; } = new();
    public List<ChatDTO> Chats { get; set; } = new();
}
