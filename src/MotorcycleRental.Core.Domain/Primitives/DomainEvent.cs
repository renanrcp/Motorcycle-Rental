using MediatR;

namespace MotorcycleRental.Core.Domain.Primitives;

public record DomainEvent(Guid Id) : INotification;
