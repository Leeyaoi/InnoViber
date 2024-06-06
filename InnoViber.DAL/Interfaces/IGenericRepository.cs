using InnoViber.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoViber.DAL.Interfaces;

public interface IGenericRepository<TEntity>
{
    public Task<List<TEntity>> GetAll();

    public Task Create(TEntity entity, CancellationToken ct);

    public Task<TEntity> Update(TEntity entity, CancellationToken ct);

    public Task Delete(TEntity entity, CancellationToken ct);
}
