using InnoViber.BLL.Models;
using InnoViber.Domain.Enums;

namespace InnoViber.BLL.Interfaces;

public interface IMessageService : IGenericService<MessageModel>
{
    Task<MessageModel> UpdateStatus(MessageStatus status, MessageModel model, CancellationToken ct);

    Task<List<MessageModel>> GetByChatId(Guid chatId, CancellationToken ct);

    Task<PaginatedModel<MessageModel>> PaginateByChatId(Guid chatId, int limit, int page, CancellationToken ct);
}
