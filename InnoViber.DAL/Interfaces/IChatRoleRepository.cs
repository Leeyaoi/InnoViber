using InnoViber.DAL.Entities;

namespace InnoViber.DAL.Interfaces;

public interface IChatRoleRepository : IGenericRepository<ChatRoleEntity>
{
    Task<List<ChatRoleEntity>> PaginateByChatId(Guid chatId, int limit, int page, CancellationToken ct, out int total);
}
