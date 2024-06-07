using AutoMapper;
using InnoViber.BLL.Interfaces;
using InnoViber.BLL.Models;
using InnoViber.DAL.Interfaces;
using InnoViber.DAL.Models;

namespace InnoViber.BLL.Services;

public class MessageService : GenericService<MessageModel, MessageEntity>, IMessageService
{
    public MessageService(IMapper mapper, IMessageRepository repository) : base(mapper, repository)
    { }
}
