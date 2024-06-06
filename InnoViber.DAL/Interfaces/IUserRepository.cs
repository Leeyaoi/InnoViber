using InnoViber.DAL.Models;

namespace InnoViber.DAL.Interfaces;

public interface IUserRepository : IGenericRepository<User>
{
    public Task<User?> GetById(Guid id);
}
