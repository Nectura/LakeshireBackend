using System.Linq.Expressions;
using Lakeshire.Common.DAL.DataContexts;
using Lakeshire.Common.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lakeshire.Common.DAL.Repositories;

public class EntityRepository<T> : IEntityRepository<T> where T : class
{
    protected readonly EntityContext _context;
    
    public EntityRepository(EntityContext context)
    {
        _context = context;
    }

    public async Task<T?> FindAsync<TPK>(TPK primaryKey, CancellationToken cancellationToken = default)
    {
        return await _context.Set<T>().FindAsync(new object?[] { primaryKey }, cancellationToken: cancellationToken);
    }

    public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Set<T>().ToListAsync(cancellationToken);
    }

    public IQueryable<T> Query(Expression<Func<T, bool>> expression)
    {
        return _context.Set<T>().Where(expression);
    }

    public void Add(T entity)
    {
        _context.Set<T>().Add(entity);
    }

    public void AddRange(IEnumerable<T> entities)
    {
        _context.Set<T>().AddRange(entities);
    }

    public void Remove(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        _context.Set<T>().RemoveRange(entities);
    }
}