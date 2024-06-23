using Microsoft.EntityFrameworkCore;
using MotorcycleRental.Core.Domain.Abstractions;
using MotorcycleRental.Core.Infrastructure.Contexts;
using MotorcycleRental.Deliverers.Domain.Entities;

namespace MotorcycleRental.Deliverers.Infrastructure.Contexts;

public class DeliverersDbContext(DbContextOptions options, IDomainEventSaver domainEventSaver)
    : ApplicationContext(options, domainEventSaver), IDeliverersDbContext
{
    public DbSet<Deliverer> Deliverers { get; set; }

    public DbSet<DelivererImage> DelivererImages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DeliverersDbContext).Assembly);
    }
}
