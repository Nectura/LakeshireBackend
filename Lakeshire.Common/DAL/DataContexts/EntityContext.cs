using Lakeshire.Common.DAL.DataContexts.Interfaces;
using Lakeshire.Common.DAL.Models;
using Microsoft.EntityFrameworkCore;

#pragma warning disable 8618

namespace Lakeshire.Common.DAL.DataContexts;

public sealed class EntityContext : DbContext, IEntityContext
{
    public DbSet<UserAccount> UserAccounts { get; set; }
    public DbSet<UserAccountServiceAuth> UserAccountServiceAuths { get; set; }
    public DbSet<UserPost> UserPosts { get; set; }
    //public DbSet<UserPostComment> UserPostComments { get; set; }
    //public DbSet<UserPostLike> UserPostLikes { get; set; }
    //public DbSet<UserPostShare> UserPostShares { get; set; }

    public EntityContext()
    {
    }

    public EntityContext(DbContextOptions<EntityContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<UserAccount>()
            .HasKey(m => m.Id);
        builder.Entity<UserAccount>()
            .HasMany(m => m.Posts)
            .WithOne(m => m.User)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Entity<UserAccount>()
            .HasMany(m => m.ServiceAuths)
            .WithOne(m => m.User)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<UserPost>()
            .HasKey(m => m.Id);
        builder.Entity<UserPost>()
            .Property(m => m.Content)
            .HasMaxLength(280);
        //builder.Entity<UserPost>()
        //    .HasMany(m => m.PostComments)
        //    .WithOne(m => m.Post)
        //    .OnDelete(DeleteBehavior.SetNull);
        //builder.Entity<UserPost>()
        //    .HasMany(m => m.PostLikes)
        //    .WithOne(m => m.Post)
        //    .OnDelete(DeleteBehavior.Cascade);
        //builder.Entity<UserPost>()
        //    .HasMany(m => m.PostShares)
        //    .WithOne(m => m.SharedPost)
        //    .OnDelete(DeleteBehavior.SetNull);

        //builder.Entity<UserPostComment>()
        //    .HasKey(m => new { m.PostId, m.CommentPostId });
            
        //builder.Entity<UserPostLike>()
        //    .HasKey(m => new { m.UserId, m.PostId });
        
        //builder.Entity<UserPostShare>()
        //    .HasKey(m => m.Id);
        //builder.Entity<UserPostShare>()
        //    .Property(m => m.Id)
        //    .HasDefaultValueSql("NEWID()");
    }
}