using InnoViber.DAL.Models;

namespace InnoViber.DAL.Interfaces;

internal interface IMessageRepository : IGenericRepository<Message>
{
    public Task<Message?> GetById(Guid id);
}
