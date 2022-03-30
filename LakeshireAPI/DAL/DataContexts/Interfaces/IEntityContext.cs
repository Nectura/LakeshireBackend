using LakeshireAPI.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace LakeshireAPI.DAL.DataContexts.Interfaces;

public interface IEntityContext
{
    DbSet<UserAccount> UserAccounts { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}