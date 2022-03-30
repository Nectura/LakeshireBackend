using Lakeshire.Common.DAL.DataContexts;
using Lakeshire.Common.DAL.Repositories;
using Lakeshire.Common.DAL.Repositories.Interfaces;
using Lakeshire.Common.DAL.WorkUnits.Interfaces;

namespace Lakeshire.Common.DAL.WorkUnits;

public sealed class AuthWorkUnit : UnitOfWork, IAuthWorkUnit
{
    public AuthWorkUnit(EntityContext entityContext) : base(entityContext)
    {
        UserAccounts = new UserAccountRepository(entityContext);
        UserAccountAuths = new UserAccountAuthRepository(entityContext);
    }
    
    public IUserAccountRepository UserAccounts { get; }
    public IUserAccountAuthRepository UserAccountAuths { get; }
}