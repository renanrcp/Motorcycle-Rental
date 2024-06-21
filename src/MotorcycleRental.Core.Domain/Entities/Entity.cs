using MotorcycleRental.Core.Domain.Primitives;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotorcycleRental.Core.Domain.Entities;

public abstract class Entity
{
    private readonly List<DomainEvent> _domainEvents = [];

    public int Id { get; }

    [NotMapped]
    public IReadOnlyList<DomainEvent> DomainEvents => _domainEvents;

    protected void Raise(DomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}
