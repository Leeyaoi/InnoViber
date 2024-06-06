using InnoViber.DAL.Data;
using InnoViber.DAL.Interfaces;
using InnoViber.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace InnoViber.DAL.Repositories;

internal class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(ViberContext context) : base(context)
    {}

    new public Task<User?> GetById(Guid Id) => _dbSet.Where(x => x.Id == Id).Include(x => x.Chats).FirstOrDefaultAsync();
}
