using InnoViber.DAL.Data;
using InnoViber.DAL.Interfaces;
using InnoViber.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace InnoViber.DAL.Repositories;

internal class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(ViberContext context) : base(context)
    {}

    public Task<User?> GetById(Guid id)
    {
        return _dbSet.Where(x => x.Id == id).Include(x => x.Chats).FirstOrDefaultAsync();
    }
}
