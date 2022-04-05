using System.Linq.Expressions;

namespace Lakeshire.Common.DAL.Repositories.Interfaces;

public interface IEntityRepository<T> where T : class
{
    Task<bool> AnyAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
    Task<T?> FindAsync<TPK>(TPK primaryKey, CancellationToken cancellationToken = default);
    Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default);
    IQueryable<T> Query(Expression<Func<T, bool>> expression);
    void Add(T entity);
    void AddRange(IEnumerable<T> entities);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
}