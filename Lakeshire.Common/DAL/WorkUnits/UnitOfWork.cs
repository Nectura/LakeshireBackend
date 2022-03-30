using Lakeshire.Common.DAL.DataContexts;
using Lakeshire.Common.DAL.WorkUnits.Interfaces;

namespace Lakeshire.Common.DAL.WorkUnits;

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