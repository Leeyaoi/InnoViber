using InnoViber.BLL.Models;

namespace InnoViber.BLL.Interfaces;

public interface IChatService : IGenericService<ChatModel>
{
    Task<ChatModel> UpdateUsersList(Guid id, Guid UserId, CancellationToken ct);
}
