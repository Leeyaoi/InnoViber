using InnoViber.DAL.Data;
using InnoViber.DAL.Interfaces;
using InnoViber.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace InnoViber.DAL.Repositories;

internal class UserRepo : IUser
{
    private readonly ViberContext dbContext;

    public UserRepo(ViberContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task Create(User user)
    {
        await dbContext.Users.AddAsync(user);
        await dbContext.SaveChangesAsync();
    }

    public async Task Delete(User user)
    {
        dbContext.Users.Remove(user);
        await dbContext.SaveChangesAsync();
    }

    public async Task<List<User>> GetAll()
    {
        return await dbContext.Users.ToListAsync();
    }

    public async Task<User?> GetById(Guid id)
    {
        return await dbContext.Users.Where(x => x.Id == id).Include(x => x.Chats).FirstOrDefaultAsync();
    }

    public async Task Update(User user)
    {
        dbContext.Update(user);
        await dbContext.SaveChangesAsync();
    }
}
