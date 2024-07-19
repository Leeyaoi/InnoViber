using InnoViber.API.ViewModels.Message;

namespace InnoViber.API.Interfaces;

public interface IChatClient
{
    Task RecieveMessage(MessageViewModel message);

    Task JoinChat(MessageViewModel message);
}
