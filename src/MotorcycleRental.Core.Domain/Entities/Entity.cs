using MotorcycleRental.Core.Domain.Primitives;

namespace MotorcycleRental.Core.Domain.Entities;

public abstract class Entity
{
    private readonly List<DomainEvent> _domainEvents = [];

    public int Id { get; }

    protected void Raise(DomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}
