using InnoViber.DAL.Data;
using InnoViber.DAL.Interfaces;
using InnoViber.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace InnoViber.DAL.Repositories;

public class UserRepository : GenericRepository<UserEntity>, IUserRepository
{
    public UserRepository(ViberContext context) : base(context)
    { }

    public override Task<UserEntity?> GetById(Guid Id, CancellationToken ct) => _dbSet.Where(x => x.Id == Id)
        .Include(x => x.Chats).FirstOrDefaultAsync(ct);
}
