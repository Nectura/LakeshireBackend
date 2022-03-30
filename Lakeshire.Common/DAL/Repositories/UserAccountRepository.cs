using Lakeshire.Common.DAL.DataContexts;
using Lakeshire.Common.DAL.Models;
using Lakeshire.Common.DAL.Repositories.Interfaces;

namespace Lakeshire.Common.DAL.Repositories;

public sealed class UserAccountRepository : EntityRepository<UserAccount>, IUserAccountRepository
{
    public UserAccountRepository(EntityContext context) : base(context)
    {
    }
}