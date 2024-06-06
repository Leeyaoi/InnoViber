using InnoViber.DAL.Models;

namespace InnoViber.DAL.Interfaces;

public interface IChatRepository : IGenericRepository<Chat>
{ public Task<Chat?> GetById(Guid id);
}
