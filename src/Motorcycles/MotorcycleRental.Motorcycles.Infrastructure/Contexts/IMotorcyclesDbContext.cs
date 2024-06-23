using Microsoft.EntityFrameworkCore;
using MotorcycleRental.Motorcycles.Domain.Entities;

namespace MotorcycleRental.Motorcycles.Infrastructure.Contexts;

public interface IMotorcyclesDbContext
{
    public DbSet<Motorcycle> Motorcycles { get; set; }
}
