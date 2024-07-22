using InnoViber.DAL.Entities;

namespace InnoViber.DAL.Interfaces;

public interface IMessageRepository : IGenericRepository<MessageEntity>
{
    Task<List<MessageEntity>> PaginateByChatId(Guid chatId, int limit, int page, CancellationToken ct, out int total, out int count);
}
