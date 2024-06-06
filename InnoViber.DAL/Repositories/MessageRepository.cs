using InnoViber.DAL.Data;
using InnoViber.DAL.Interfaces;
using InnoViber.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace InnoViber.DAL.Repositories;

internal class MessageRepository : GenericRepository<Message>, IMessageRepository
{
    public MessageRepository(ViberContext context) : base(context)
    { }

    public Task<Message?> GetById(Guid id)
    {
        return  _dbSet.Where(x => x.Id == id)
            .Include(x => x.User)
            .Include(x => x.Chat)
            .FirstOrDefaultAsync();
    }
}
