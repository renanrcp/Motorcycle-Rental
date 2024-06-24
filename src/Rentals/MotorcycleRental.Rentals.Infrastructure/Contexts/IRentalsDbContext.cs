using Microsoft.EntityFrameworkCore;
using MotorcycleRental.Rentals.Domain.Entities;

namespace MotorcycleRental.Rentals.Infrastructure.Contexts;

public interface IRentalsDbContext
{
    public DbSet<Rental> Rentals { get; set; }

    public DbSet<RentalType> RentalTypes { get; set; }
}
