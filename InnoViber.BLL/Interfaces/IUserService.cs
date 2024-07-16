using InnoViber.BLL.Models;
using InnoViber.User.DAL.Models;

namespace InnoViber.BLL.Interfaces;

public interface IUserService : IGenericService<UserModel>
{
    Task<List<UserModel>> GetByMongoId(Guid mongoId, CancellationToken ct);

    Task<ExternalUserModel> GetOrCreate(ShortExternalUserModel userModel, CancellationToken ct);
}
