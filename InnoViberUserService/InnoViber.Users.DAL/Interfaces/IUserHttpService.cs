using UserService.ExternalUsers.DAL.Models;

namespace UserService.ExternalUsers.DAL.Interfaces;

public interface IUserHttpService
{
    Task<HttpResponseMessage> CreateUser(ExternalUserModel model, CancellationToken ct);
}
