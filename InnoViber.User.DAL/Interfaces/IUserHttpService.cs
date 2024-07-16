using InnoViber.User.DAL.Models;

namespace InnoViber.User.DAL.Interfaces;

public interface IUserHttpService
{
    Task<ExternalUserModel?> GetUser(Guid userId, CancellationToken ct);
    Task<ExternalUserModel?> GetUserByAuthId(string authId, CancellationToken ct);
    Task<HttpResponseMessage> PostUser(ShortExternalUserModel user, CancellationToken ct);
}
