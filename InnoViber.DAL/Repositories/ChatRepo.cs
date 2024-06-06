using InnoViber.DAL.Data;
using InnoViber.DAL.Interfaces;
using InnoViber.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace InnoViber.DAL.Repositories;

internal class ChatRepo : IChat
{
    private readonly ViberContext dbContext;

    public ChatRepo(ViberContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task Create(Chat chat)
    {
        await dbContext.Chats.AddAsync(chat);
        await dbContext.SaveChangesAsync();
    }

    public async Task Delete(Chat chat)
    {
        dbContext.Chats.Remove(chat);
        await dbContext.SaveChangesAsync();
    }

    public async Task<List<Chat>> GetAll()
    {
        return await dbContext.Chats.ToListAsync();
    }

    public async Task<Chat?> GetById(Guid id)
    {
        return await dbContext.Chats.Where(x => x.Id == id)
            .Include(x => x.Users)
            .Include(x => x.Messages)
            .FirstOrDefaultAsync();
    }

    public async Task Update(Chat chat)
    {
        dbContext.Chats.Update(chat);
        await dbContext.SaveChangesAsync();
    }
}
