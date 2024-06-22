using MotorcycleRental.Core.Domain.Primitives;

namespace MotorcycleRental.Core.Domain.Abstractions;

public interface IDomainEventSaver
{
    Task SaveEventAsync(DomainEvent domainEvent);
}
