using AutoMapper;
using InnoViber.BLL.Interfaces;
using InnoViber.BLL.Models;
using InnoViber.DAL.Interfaces;
using InnoViber.DAL.Models;
using InnoViber.Domain.Providers;
using InnoViber.Domain.Enums;
using InnoViber.DAL.Data;

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
        await _repository.Create(entity, ct);
        return model;
    }

    public override async Task<MessageModel> Update(Guid id, MessageModel model, CancellationToken ct)
    {
        model.Id = id;
        var entity = _mapper.Map<MessageEntity>(model);
        await _repository.Update(entity, ct);
        return model;
    }

    public Task UpdateStatus(MessageStatus status, MessageModel model, CancellationToken ct)
    {
        model.Status = status;
        var entity = _mapper.Map<MessageEntity>(model);
        return _repository.Update(entity, ct);
    }
}
