using MotorcycleRental.Core.Domain.Abstractions;
using MotorcycleRental.Core.Domain.Primitives;

namespace MotorcycleRental.Core.Infrastructure.Contexts.Tests;

public class TestDomainEventSaver : IDomainEventSaver
{
    private readonly List<DomainEvent> _domainEvents = [];

    public IReadOnlyList<DomainEvent> DomainEvents => _domainEvents;

    public Task SaveEventAsync(DomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);

        return Task.CompletedTask;
    }
}
