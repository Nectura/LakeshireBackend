using LakeshireAPI.DAL.Repositories.Interfaces;

namespace LakeshireAPI.DAL.WorkUnits.Interfaces;

public interface IAuthWorkUnit : IUnitOfWork
{
    IUserAccountRepository UserAccounts { get; }
}