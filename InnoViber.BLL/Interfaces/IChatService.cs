using InnoViber.BLL.Models;

namespace InnoViber.BLL.Interfaces;

public interface IChatService : IGenericService<ChatModel>
{
    Task<List<ChatModel>> GetByUserId(string userId, CancellationToken ct);

    Task<PaginatedModel<ChatModel>> PaginateByUserId(string userId, int limit, int page, CancellationToken ct);
}
