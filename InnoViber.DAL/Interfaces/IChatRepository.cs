using InnoViber.DAL.Entities;

namespace InnoViber.DAL.Interfaces;

public interface IChatRepository : IGenericRepository<ChatEntity>
{
    Task<List<ChatEntity>> PaginateByUserId(string UserId, int limit, int page, CancellationToken ct, out int total);
}
