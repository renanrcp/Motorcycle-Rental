using MotorcycleRental.Core.Domain.Primitives;

namespace MotorcycleRental.Deliverers.Domain.Events;

public record DelivererImageCreatedEvent : DomainEvent
{
    public required int DelivererId { get; init; }

    public required string Path { get; init; }
}
