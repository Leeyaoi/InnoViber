using InnoViber.DAL.Models;

namespace InnoViber.DAL.Interfaces;

public interface IChat
{
    public Task<List<Chat>> GetAll();

    public Task<Chat?> GetById(Guid id);

    public Task Create(Chat chat);

    public Task Update(Chat chat);

    public Task Delete(Chat chat);
}
