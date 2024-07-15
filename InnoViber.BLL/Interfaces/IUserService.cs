using InnoViber.BLL.Models;

namespace InnoViber.BLL.Interfaces;

public interface IUserService : IGenericService<UserModel>
{
    Task<List<UserModel>> GetByMongoId(Guid mongoId, CancellationToken ct);
}
