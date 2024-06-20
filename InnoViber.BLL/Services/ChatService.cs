using AutoMapper;
using InnoViber.BLL.Interfaces;
using InnoViber.BLL.Models;
using InnoViber.DAL.Interfaces;
using InnoViber.DAL.Entities;

namespace InnoViber.BLL.Services;

public class ChatService : GenericService<ChatModel, ChatEntity>, IChatService
{
    public ChatService(IMapper mapper, IChatRepository repository) : base(mapper, repository)
    { }
}
