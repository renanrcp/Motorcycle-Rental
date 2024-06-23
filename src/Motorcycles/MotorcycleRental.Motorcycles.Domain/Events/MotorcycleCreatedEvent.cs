using MotorcycleRental.Core.Domain.Primitives;

namespace MotorcycleRental.Motorcycles.Domain.Events;

public record MotorcycleCreatedEvent : DomainEvent
{
    public required string LicensePlate { get; init; }
}
