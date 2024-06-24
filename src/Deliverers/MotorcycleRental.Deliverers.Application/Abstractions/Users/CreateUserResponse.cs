using MotorcycleRental.Core.Domain.Entities;

namespace MotorcycleRental.Deliverers.Application.Abstractions.Users;

public class CreateUserResponse
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public required string Email { get; init; }

    public required IEnumerable<PermissionType> Permissions { get; init; }
}
