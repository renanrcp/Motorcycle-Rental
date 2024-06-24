namespace MotorcycleRental.Deliverers.Application.Abstractions.Users;

public class CreateUserRequest
{
    public required string Name { get; init; }

    public required string Email { get; init; }

    public required string Password { get; init; }
}
