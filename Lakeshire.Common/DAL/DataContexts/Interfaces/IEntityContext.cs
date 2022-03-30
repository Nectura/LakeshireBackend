using Lakeshire.Common.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Lakeshire.Common.DAL.DataContexts.Interfaces;

public interface IEntityContext
{
    DbSet<UserAccount> UserAccounts { get; set; }
    DbSet<UserAccountServiceAuth> UserAccountServiceAuths { get; set; }
    DbSet<UserPost> UserPosts { get; set; }
    //DbSet<UserPostComment> UserPostComments { get; set; }
    //DbSet<UserPostLike> UserPostLikes { get; set; }
    //DbSet<UserPostShare> UserPostShares { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}