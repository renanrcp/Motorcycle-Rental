using MotorcycleRental.Core.Domain.Primitives;

namespace MotorcycleRental.Users.Domain.Events;

public record UserCreatedEvent : DomainEvent
{
    public required string Email { get; init; }
}
