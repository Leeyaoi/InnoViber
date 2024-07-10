using AutoMapper;
using InnoViber.BLL.Interfaces;
using InnoViber.BLL.Models;
using InnoViber.DAL.Interfaces;
using InnoViber.DAL.Entities;
using InnoViber.Domain.Providers;
using InnoViber.Domain.Enums;

namespace InnoViber.BLL.Services;

public class MessageService : GenericService<MessageModel, MessageEntity>, IMessageService
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IMapper _mapper;
    private readonly IGenericRepository<MessageEntity> _repository;

    public MessageService(IMapper mapper, IMessageRepository repository, IDateTimeProvider dateTimeProvider) 
        : base(mapper, repository)
    { 
        _dateTimeProvider = dateTimeProvider;
        _mapper = mapper;
        _repository = repository;
    }

    public override async Task<MessageModel> Create(MessageModel model, CancellationToken ct)
    {
        var utcNow = _dateTimeProvider.GetDate();
        var entity = _mapper.Map<MessageEntity>(model);
        entity.Date = utcNow;
        entity.Status = MessageStatus.Send;
        var result = await _repository.Create(entity, ct);
        return _mapper.Map<MessageModel>(result);
    }

    public async Task<List<MessageModel>> GetByChatId(Guid chatId, CancellationToken ct)
    {
        var entities = await _repository.GetByPredicate(message => message.ChatId == chatId, ct);
        return _mapper.Map<List<MessageModel>>(entities);
    }

    public override async Task<MessageModel> Update(Guid id, MessageModel model, CancellationToken ct)
    {
        model.Id = id;
        var entity = _mapper.Map<MessageEntity>(model);
        var result = await _repository.Update(entity, ct);
        return _mapper.Map<MessageModel>(result);
    }

    public async Task<MessageModel> UpdateStatus(MessageStatus status, MessageModel model, CancellationToken ct)
    {
        model.Status = status;
        var entity = _mapper.Map<MessageEntity>(model);
        var result = await _repository.Update(entity, ct);
        return _mapper.Map<MessageModel>(result);
    }
}
