using Microsoft.EntityFrameworkCore;
using MotorcycleRental.Users.Domain.Entities;
using MotorcycleRental.Users.Infrastructure.Contexts;

namespace MotorcycleRental.Core.Migrator.Contexts;

public class MigrationDbContext(DbContextOptions<MigrationDbContext> options) :
    DbContext(options), IUsersDbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<Permission> Permissions { get; set; }

    public DbSet<User> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IUsersDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
