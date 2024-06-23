using MotorcycleRental.Core.Domain.Entities;

namespace MotorcycleRental.Users.Application.Commands.Users.Create;

public class CreateUserResponse
{
    public required string Name { get; init; }

    public required string Email { get; init; }

    public required IEnumerable<PermissionType> Permissions { get; init; }
}
