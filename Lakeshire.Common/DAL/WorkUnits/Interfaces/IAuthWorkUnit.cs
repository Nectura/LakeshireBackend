using Lakeshire.Common.DAL.Repositories.Interfaces;

namespace Lakeshire.Common.DAL.WorkUnits.Interfaces;

public interface IAuthWorkUnit : IUnitOfWork
{
    IUserAccountRepository UserAccounts { get; }
    IUserAccountAuthRepository UserAccountAuths { get; }
}