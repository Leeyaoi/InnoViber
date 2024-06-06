using InnoViber.DAL.Models;

namespace InnoViber.DAL.Interfaces;

public interface IUser
{
    public Task<List<User>> GetAll();

    public Task<User?> GetById(Guid id);

    public Task Create(User user);

    public Task Update(User user);

    public Task Delete(User user);
}
