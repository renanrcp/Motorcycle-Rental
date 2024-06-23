using Microsoft.EntityFrameworkCore;
using MotorcycleRental.Deliverers.Domain.Entities;

namespace MotorcycleRental.Deliverers.Infrastructure.Contexts;

public interface IDeliverersDbContext
{
    DbSet<Deliverer> Deliverers { get; set; }

    DbSet<DelivererImage> DelivererImages { get; set; }
}
