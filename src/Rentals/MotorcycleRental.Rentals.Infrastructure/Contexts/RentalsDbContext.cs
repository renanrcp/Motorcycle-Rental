using Microsoft.EntityFrameworkCore;
using MotorcycleRental.Core.Domain.Abstractions;
using MotorcycleRental.Core.Infrastructure.Contexts;
using MotorcycleRental.Rentals.Domain.Entities;

namespace MotorcycleRental.Rentals.Infrastructure.Contexts;

public class RentalsDbContext(DbContextOptions options, IDomainEventSaver domainEventSaver)
    : ApplicationContext(options, domainEventSaver), IRentalsDbContext
{
    public DbSet<Rental> Rentals { get; set; }

    public DbSet<RentalType> RentalTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RentalsDbContext).Assembly);
    }
}
