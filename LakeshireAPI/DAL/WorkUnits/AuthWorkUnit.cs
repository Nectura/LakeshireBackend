using LakeshireAPI.DAL.DataContexts;
using LakeshireAPI.DAL.Repositories;
using LakeshireAPI.DAL.Repositories.Interfaces;
using LakeshireAPI.DAL.WorkUnits.Interfaces;

namespace LakeshireAPI.DAL.WorkUnits;

public sealed class AuthWorkUnit : UnitOfWork, IAuthWorkUnit
{
    public AuthWorkUnit(EntityContext entityContext) : base(entityContext)
    {
        UserAccounts = new UserAccountRepository(entityContext);
    }
    
    public IUserAccountRepository UserAccounts { get; }
}