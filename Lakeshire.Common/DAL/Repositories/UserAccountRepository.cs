using Lakeshire.Common.DAL.DataContexts;
using Lakeshire.Common.DAL.Models;
using Lakeshire.Common.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lakeshire.Common.DAL.Repositories;

public sealed class UserAccountRepository : EntityRepository<UserAccount>, IUserAccountRepository
{
    public UserAccountRepository(EntityContext context) : base(context)
    {
    }

    public async Task<UserAccount?> FindByEmailAddressAsync(string input, CancellationToken cancellationToken = default)
    {
        return await _context.UserAccounts.FirstOrDefaultAsync(m => m.EmailAddress.ToLower().Equals(input.ToLower()), cancellationToken);
    }

    public async Task<bool> IsEmailTakenAsync(string input, CancellationToken cancellationToken = default)
    {
        return await _context.UserAccounts.AnyAsync(m => m.EmailAddress.ToLower().Equals(input.ToLower()), cancellationToken);
    }
}