using Crypto.Entities;
using Microsoft.EntityFrameworkCore;

namespace Crypto.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Tenant> Tenants => Set<Tenant>();

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>()
            .HasIndex(x => x.Email)
            .IsUnique();

        builder.Entity<User>()
            .HasOne<Tenant>()
            .WithMany(t => t.Users)
            .HasForeignKey(x => x.TenantId);
    }
}