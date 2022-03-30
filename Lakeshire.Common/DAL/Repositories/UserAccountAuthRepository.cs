using Lakeshire.Common.DAL.DataContexts;
using Lakeshire.Common.DAL.Models;
using Lakeshire.Common.DAL.Repositories.Interfaces;

namespace Lakeshire.Common.DAL.Repositories;

public sealed class UserAccountAuthRepository : EntityRepository<UserAccountServiceAuth>, IUserAccountAuthRepository
{
    public UserAccountAuthRepository(EntityContext context) : base(context)
    {
    }
}