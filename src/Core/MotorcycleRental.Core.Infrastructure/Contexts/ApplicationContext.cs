using MediatR;
using Microsoft.EntityFrameworkCore;
using MotorcycleRental.Core.Domain.Abstractions;
using MotorcycleRental.Core.Domain.Entities;

namespace MotorcycleRental.Core.Infrastructure.Contexts;

public abstract class ApplicationContext(DbContextOptions options, IDomainEventSaver domainEventSaver) : DbContext(options)
{
    private readonly IDomainEventSaver _domainEventSaver = domainEventSaver;

    protected virtual void AfterSaveChanges()
    {
        var asyncOnSaveChanges = AfterSaveChangesAsync();

        if (!asyncOnSaveChanges.IsCompletedSuccessfully)
        {
            AfterSaveChangesAsync().GetAwaiter().GetResult();
        }
    }

    protected virtual async Task AfterSaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var domainEvents = ChangeTracker
                            .Entries()
                            .Where(e => e.Entity is Entity)
                            .Select(e => (Entity)e.Entity)
                            .Where(e => e.DomainEvents.Count > 0)
                            .SelectMany(e => e.DomainEvents);

        if (!domainEvents.Any())
        {
            return;
        }

        foreach (var domainEvent in domainEvents)
        {
            await _domainEventSaver.SaveEventAsync(domainEvent, cancellationToken);
        }
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        var records = base.SaveChanges(acceptAllChangesOnSuccess);

        AfterSaveChanges();

        return records;
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        var records = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);

        await AfterSaveChangesAsync(cancellationToken);

        return records;
    }
}
