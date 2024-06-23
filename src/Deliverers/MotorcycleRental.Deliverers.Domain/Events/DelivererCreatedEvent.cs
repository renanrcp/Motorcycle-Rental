using MotorcycleRental.Core.Domain.Primitives;

namespace MotorcycleRental.Deliverers.Domain.Events;

public record DelivererCreatedEvent : DomainEvent
{
    public required int DelivererId { get; init; }
}
