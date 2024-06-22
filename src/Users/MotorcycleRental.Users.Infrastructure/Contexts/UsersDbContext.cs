using MediatR;
using Microsoft.EntityFrameworkCore;
using MotorcycleRental.Core.Domain.Abstractions;
using MotorcycleRental.Core.Infrastructure.Contexts;
using MotorcycleRental.Users.Domain.Entities;

namespace MotorcycleRental.Users.Infrastructure.Contexts;

public class UsersDbContext(DbContextOptions options, IPublisher publisher, IDomainEventSaver domainEventSaver)
    : ApplicationContext(options, publisher, domainEventSaver)
{
    public DbSet<User> Users { get; set; }

    public DbSet<Permission> Permissions { get; set; }

    public DbSet<User> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UsersDbContext).Assembly);
    }
}
