using InnoViber.API.ViewModels.Chat;

namespace InnoViber.Test.Integration.Data;

public static class ChatViewModels
{
    public static ChatShortViewModel ShortChat = new()
    {
        Name = "123456"
    };

    public static ChatViewModel Chat = new()
    {
        Id = Guid.NewGuid(),
        Name = "123456"
    };
}
