using MotorcycleRental.Core.Domain.Primitives;

namespace MotorcycleRental.Motorcycles.Domain.Events;

public record MotorcycleLicensePlateUpdatedEvent : DomainEvent
{
    public required int MotorcycleId { get; init; }

    public required string LicensePlate { get; init; }
}
