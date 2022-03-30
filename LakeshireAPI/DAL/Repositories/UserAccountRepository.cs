using LakeshireAPI.DAL.DataContexts;
using LakeshireAPI.DAL.Models;
using LakeshireAPI.DAL.Repositories.Interfaces;

namespace LakeshireAPI.DAL.Repositories;

public sealed class UserAccountRepository : EntityRepository<UserAccount>, IUserAccountRepository
{
    public UserAccountRepository(EntityContext context) : base(context)
    {
    }
}