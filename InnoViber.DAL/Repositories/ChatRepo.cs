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
        return await dbContext.Chats.Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public List<Message> GetMessages(Guid id)
    {
        return dbContext.Chats.Where(x => x.Id == id)
            .Include(x => x.Messages)
            .FirstOrDefault().Messages;
    }

    public Task<List<User>> GetUsers(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task Update(Chat chat)
    {
        throw new NotImplementedException();
    }
}
