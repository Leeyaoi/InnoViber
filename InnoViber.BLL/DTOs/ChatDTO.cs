namespace InnoViber.BLL.Models;

public class ChatDTO : BaseDTO
{
    public string Name { get; set; } = string.Empty;
    List<UserDTO> Users { get; set; } = new();
    List<MessageDTO> Messages { get; set; } = new();
}
