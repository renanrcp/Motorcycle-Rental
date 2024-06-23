using Microsoft.EntityFrameworkCore;
using MotorcycleRental.Motorcycles.Domain.Entities;
using MotorcycleRental.Motorcycles.Infrastructure.Contexts;
using MotorcycleRental.Users.Domain.Entities;
using MotorcycleRental.Users.Infrastructure.Contexts;

namespace MotorcycleRental.Core.Migrator.Contexts;

public class MigrationDbContext(DbContextOptions<MigrationDbContext> options) :
    DbContext(options), IUsersDbContext, IMotorcyclesDbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<Permission> Permissions { get; set; }

    public DbSet<User> Roles { get; set; }

    public DbSet<Motorcycle> Motorcycles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IUsersDbContext).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IMotorcyclesDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
