using LakeshireAPI.DAL.DataContexts;
using LakeshireAPI.DAL.WorkUnits.Interfaces;

namespace LakeshireAPI.DAL.WorkUnits;

public abstract class UnitOfWork : IUnitOfWork
{
    protected readonly EntityContext _entityContext;

    protected UnitOfWork(EntityContext entityContext)
    {
        _entityContext = entityContext;
    }

    public async ValueTask DisposeAsync()
    {
        await _entityContext.DisposeAsync();
        GC.SuppressFinalize(this);
    }

    public async Task<int> CommitChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _entityContext.SaveChangesAsync(cancellationToken);
    }
}