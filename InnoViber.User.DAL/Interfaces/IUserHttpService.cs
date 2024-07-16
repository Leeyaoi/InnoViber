using InnoViber.User.DAL.Models;

namespace InnoViber.User.DAL.Interfaces;

public interface IUserHttpService
{
    Task<ExternalUserModel?> GetUser(string userId, CancellationToken ct);
}
