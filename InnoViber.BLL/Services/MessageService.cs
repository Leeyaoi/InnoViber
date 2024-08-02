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
    private readonly IMessageRepository _repository;

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
        entity.Status = MessageStatus.Delivered;
        var result = await _repository.Create(entity, ct);
        return _mapper.Map<MessageModel>(result);
    }

    public async Task<List<MessageModel>> GetByChatId(Guid chatId, CancellationToken ct, string? userId)
    {
        var entities = await _repository.GetByPredicate(message => message.ChatId == chatId, ct);
        return await UpdateStatus(entities!, ct, userId);
    }

    public async Task<PaginatedModel<MessageModel>> PaginateByChatId(Guid chatId, int limit, int page, CancellationToken ct, string? userId, bool update = true)
    {
        var entities = await _repository.PaginateByChatId(chatId, limit, page, ct, out int total, out int count);
        var models = await UpdateStatus(entities!, ct, userId, update);
        return new PaginatedModel<MessageModel>
        {
            Total = total,
            Limit = limit,
            Page = page,
            Count = count,
            Items = models
        };
    }

    public override async Task<MessageModel> Update(Guid id, MessageModel model, CancellationToken ct)
    {
        model.Id = id;
        var entity = _mapper.Map<MessageEntity>(model);
        var result = await _repository.Update(entity, ct);
        return _mapper.Map<MessageModel>(result);
    }

    public async Task<List<MessageModel>> UpdateStatus( List<MessageEntity> entities, CancellationToken ct, string? userId, bool update = true)
    {
        foreach (var entity in entities)
        {
            var this_id = entity.UserId;

            if (entity.Status == MessageStatus.Delivered && this_id != userId && update)
            {
                entity.Status = MessageStatus.Read;
                await _repository.Update(entity, ct);
            }
        }
        return _mapper.Map<List<MessageModel>>(entities);
    }
}
