using MediatR;
using Microsoft.EntityFrameworkCore;
using MotorcycleRental.Core.Domain.Abstractions;
using MotorcycleRental.Core.Domain.Entities;

namespace MotorcycleRental.Core.Infrastructure.Contexts;

public abstract class ApplicationContext(DbContextOptions options, IPublisher publisher, IDomainEventSaver domainEventSaver) : DbContext(options)
{
    private readonly IPublisher _publisher = publisher;

    private readonly IDomainEventSaver _domainEventSaver = domainEventSaver;

    protected virtual void OnSaveChanges()
    {
        var asyncOnSaveChanges = OnSaveChangesAsync();

        if (!asyncOnSaveChanges.IsCompletedSuccessfully)
        {
            OnSaveChangesAsync().GetAwaiter().GetResult();
        }
    }

    protected virtual async Task OnSaveChangesAsync()
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
            await _domainEventSaver.SaveEventAsync(domainEvent);
            await _publisher.Publish(domainEvent);
        }
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        OnSaveChanges();

        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        await OnSaveChangesAsync();

        return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
}
