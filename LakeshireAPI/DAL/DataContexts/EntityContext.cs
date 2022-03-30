using LakeshireAPI.DAL.DataContexts.Interfaces;
using LakeshireAPI.DAL.Models;
using Microsoft.EntityFrameworkCore;

#pragma warning disable 8618

namespace LakeshireAPI.DAL.DataContexts;

public sealed class EntityContext : DbContext, IEntityContext
{
    public DbSet<UserAccount> UserAccounts { get; set; }

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
            .Property(m => m.Id)
            .HasDefaultValueSql("NEWID()");
    }
}