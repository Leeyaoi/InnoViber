using InnoViber.DAL.Data;
using InnoViber.DAL.Interfaces;
using InnoViber.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace InnoViber.DAL.Repositories;

internal class ChatRepository : GenericRepository<Chat>, IChatRepository
{
    public ChatRepository(ViberContext context) : base(context)
    { }

    new public Task<Chat?> GetById(Guid Id) => _dbSet.Where(x => x.Id == Id)
            .Include(x => x.Users)
            .Include(x => x.Messages)
            .FirstOrDefaultAsync();
}
