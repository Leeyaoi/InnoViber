namespace InnoViber.API.ViewModels.Chat;

public class CreateChatViewModel
{
    public string Name { get; set; } = string.Empty;
    public Guid UserId { get; set; }
}