using Microsoft.EntityFrameworkCore;
using MotorcycleRental.Deliverers.Domain.Entities;
using MotorcycleRental.Deliverers.Infrastructure.Contexts;
using MotorcycleRental.Motorcycles.Domain.Entities;
using MotorcycleRental.Motorcycles.Infrastructure.Contexts;
using MotorcycleRental.Rentals.Domain.Entities;
using MotorcycleRental.Rentals.Infrastructure.Contexts;
using MotorcycleRental.Users.Domain.Entities;
using MotorcycleRental.Users.Infrastructure.Contexts;

namespace MotorcycleRental.Core.Migrator.Contexts;

public class MigrationDbContext(DbContextOptions<MigrationDbContext> options) :
    DbContext(options), IUsersDbContext, IMotorcyclesDbContext, IDeliverersDbContext, IRentalsDbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<Permission> Permissions { get; set; }

    public DbSet<User> Roles { get; set; }

    public DbSet<Motorcycle> Motorcycles { get; set; }

    public DbSet<Deliverer> Deliverers { get; set; }

    public DbSet<DelivererImage> DelivererImages { get; set; }

    public DbSet<Rental> Rentals { get; set; }

    public DbSet<RentalType> RentalTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IUsersDbContext).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IMotorcyclesDbContext).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IDeliverersDbContext).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IRentalsDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
