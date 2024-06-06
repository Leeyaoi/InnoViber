﻿using InnoViber.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InnoViber.DAL.Interfaces;

public interface IGenericRepository<TEntity>
{
    public Task<List<TEntity>> GetAll(CancellationToken ct);

    public Task Create(TEntity entity, CancellationToken ct);

    public Task<TEntity> Update(TEntity entity, CancellationToken ct);

    public Task<TEntity?> GetByPredicate(Expression<Func<TEntity, bool>> predicate, CancellationToken ct);

    public Task<TEntity?> GetById(Guid Id, CancellationToken ct);

    public Task Delete(TEntity entity, CancellationToken ct);
}
