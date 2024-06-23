using Microsoft.EntityFrameworkCore;
using MotorcycleRental.Users.Domain.Entities;

namespace MotorcycleRental.Users.Infrastructure.Contexts;

public interface IUsersDbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<Permission> Permissions { get; set; }

    public DbSet<User> Roles { get; set; }
}
