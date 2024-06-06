using InnoViber.DAL.Data;
using InnoViber.DAL.Interfaces;
using InnoViber.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace InnoViber.DAL.Repositories;

internal class MessageRepository : GenericRepository<Message>, IMessageRepository
{
    public MessageRepository(ViberContext context) : base(context)
    { }

    new public Task<Message?> GetById(Guid Id) => _dbSet.Where(x => x.Id == Id)
            .Include(x => x.User)
            .Include(x => x.Chat)
            .FirstOrDefaultAsync();
}
