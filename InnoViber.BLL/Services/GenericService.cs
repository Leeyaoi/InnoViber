﻿using AutoMapper;
using InnoViber.BLL.Interfaces;
using InnoViber.BLL.Models;
using InnoViber.DAL.Interfaces;
using InnoViber.DAL.Models;
using System.Linq.Expressions;

namespace InnoViber.BLL.Services;

public class GenericService<TModel, TEntity> : IGenericService<TModel> where TModel : BaseModel where TEntity : BaseEntity
{
    private readonly IMapper _mapper;

    private readonly IGenericRepository<TEntity> _repository;

    public GenericService(IMapper mapper, IGenericRepository<TEntity> repository)
    {
        this._mapper = mapper;
        this._repository = repository;
    }

    public Task Create(TModel model, CancellationToken ct)
    {
        var entity = _mapper.Map<TEntity>(model);
        return _repository.Create(entity, ct);
    }

    public Task Delete(TModel model, CancellationToken ct)
    {
        var entity = _mapper.Map<TEntity>(model);
        return _repository.Delete(entity, ct);
    }

    public Task Update(Guid id, TModel model, CancellationToken ct)
    {
        model.Id = id;
        var entity = _mapper.Map<TEntity>(model);
        return _repository.Update(entity, ct);
    }

    public async Task<List<TModel>> GetAll(CancellationToken ct)
    {
        var entities = await _repository.GetAll(ct);
        return _mapper.Map<List<TModel>>(entities);
    }

    public async Task<TModel?> GetById(Guid id, CancellationToken ct)
    {
        var entity = await _repository.GetById(id, ct);
        return _mapper?.Map<TModel>(entity);
    }

    public async Task<TModel?> GetByPredicate(Expression<Func<TModel, bool>> predicate, CancellationToken ct)
    {
        var entityPred = _mapper.Map<Expression<Func<TEntity, bool>>>(predicate);
        var entity = await _repository.GetByPredicate(entityPred, ct);
        return _mapper.Map<TModel>(entity);
    }
}
