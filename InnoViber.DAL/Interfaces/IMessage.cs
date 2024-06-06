using InnoViber.DAL.Models;

namespace InnoViber.DAL.Interfaces;

internal interface IMessage
{
    public Task<List<Message>> GetAll();

    public Task<Message?> GetById(Guid id);

    public Task Create(Message message);

    public Task Update(Message message);

    public Task Delete(Message message);
}
