namespace Lakeshire.Common.DAL.WorkUnits.Interfaces;

public interface IUnitOfWork : IAsyncDisposable
{
    Task<int> CommitChangesAsync(CancellationToken cancellationToken = default);
}