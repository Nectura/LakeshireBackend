using Lakeshire.Common.DAL.Models;

namespace Lakeshire.Common.DAL.Repositories.Interfaces;

public interface IUserAccountRepository : IEntityRepository<UserAccount>
{
    Task<UserAccount?> FindByEmailAddressAsync(string input, CancellationToken cancellationToken = default);
    Task<bool> IsEmailTakenAsync(string input, CancellationToken cancellationToken = default);
}