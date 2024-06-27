using InnoViber.User.DAL.Models;

namespace InnoViber.User.DAL.Interfaces;

public interface IUserHttpService
{
    Task<List<ExternalUserModel>?> GetAllUsers(CancellationToken ct);
    Task<ExternalUserModel?> GetUser(Guid userId, CancellationToken ct);
}
