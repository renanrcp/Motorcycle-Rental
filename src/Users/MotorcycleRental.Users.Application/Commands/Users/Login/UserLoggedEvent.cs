using MotorcycleRental.Core.Application.Abstractions;

namespace MotorcycleRental.Users.Application.Commands.Users.Login;

public record UserLoggedEvent : ApplicationEvent
{
    public required int UserId { get; init; }
}
