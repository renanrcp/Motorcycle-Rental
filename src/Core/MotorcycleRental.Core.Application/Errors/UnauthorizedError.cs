using MotorcycleRental.Core.Domain.Abstractions;

namespace MotorcycleRental.Core.Application.Errors;

public record UnauthorizedError : Error
{
    public static readonly UnauthorizedError DefaultUnauthorized = new("MotorcycleRental.Unathorized", "User not authorized to access this resource.");

    public UnauthorizedError(string Code, string? Description = null) : base(Code, Description)
    {
    }
}
