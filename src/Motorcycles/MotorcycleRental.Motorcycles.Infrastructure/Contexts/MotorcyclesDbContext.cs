using Microsoft.EntityFrameworkCore;
using MotorcycleRental.Core.Domain.Abstractions;
using MotorcycleRental.Core.Infrastructure.Contexts;
using MotorcycleRental.Motorcycles.Domain.Entities;

namespace MotorcycleRental.Motorcycles.Infrastructure.Contexts;

public class MotorcyclesDbContext(DbContextOptions options, IDomainEventSaver domainEventSaver)
        : ApplicationContext(options, domainEventSaver), IMotorcyclesDbContext
{
    public DbSet<Motorcycle> Motorcycles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MotorcyclesDbContext).Assembly);
    }
}
