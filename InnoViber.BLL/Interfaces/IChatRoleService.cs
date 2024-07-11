using InnoViber.BLL.Models;
using InnoViber.Domain.Enums;

namespace InnoViber.BLL.Interfaces;

public interface IChatRoleService : IGenericService<ChatRoleModel>
{
    Task<ChatRoleModel> UpdateRole(UserRoles role, ChatRoleModel model, CancellationToken ct);

    Task<List<ChatRoleModel>> GetByChatId(Guid chatId, CancellationToken ct);
}
