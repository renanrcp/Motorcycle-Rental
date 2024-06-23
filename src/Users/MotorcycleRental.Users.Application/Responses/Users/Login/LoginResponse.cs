using MotorcycleRental.Core.Domain.Entities;
using MotorcycleRental.Users.Domain.Entities;

namespace MotorcycleRental.Users.Application.Responses.Users.Login;

public class LoginResponse
{
    public required string Name { get; init; }

    public required string Email { get; init; }

    public required string Token { get; init; }

    public required IEnumerable<PermissionType> Permissions { get; init; }
}
