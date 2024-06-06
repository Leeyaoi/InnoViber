using InnoViber.DAL.Data;
using InnoViber.DAL.Interfaces;
using InnoViber.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace InnoViber.DAL.Repositories;

internal class MessageRepo : IMessage
{
    private readonly ViberContext dbContext;

    public MessageRepo(ViberContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task Create(Message message)
    {
        await dbContext.Messages.AddAsync(message);
        await dbContext.SaveChangesAsync();
    }

    public async Task Delete(Message message)
    {
        dbContext.Messages.Remove(message);
        await dbContext.SaveChangesAsync();
    }

    public async Task<List<Message>> GetAll()
    {
        return await dbContext.Messages.ToListAsync();
    }

    public async Task<Message?> GetById(Guid id)
    {
        return await dbContext.Messages.Where(x => x.Id == id)
            .Include(x => x.User)
            .Include(x => x.Chat)
            .FirstOrDefaultAsync();
    }

    public async Task Update(Message message)
    {
        dbContext.Update(message);
        await dbContext.SaveChangesAsync();
    }
}
