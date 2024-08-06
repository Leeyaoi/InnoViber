using InnoViber.BLL.Models;
using InnoViber.Domain.Enums;

namespace InnoViber.BLL.Interfaces;

public interface IMessageService : IGenericService<MessageModel>
{
    Task<List<MessageModel>> GetByChatId(Guid chatId, CancellationToken ct, string? userId);

    Task<PaginatedModel<MessageModel>> PaginateByChatId(Guid chatId, int limit, int page, CancellationToken ct, string? userId, bool update = true);
}
